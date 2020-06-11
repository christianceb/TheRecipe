using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace TheRecipe
{
  /// <summary>
  /// Interaction logic for ViewRecipes.xaml
  /// </summary>
  public partial class ViewRecipes : Window
  {
    public RecipesModel Db = new RecipesModel();
    public FavoritesLocalStorage FavoritesLocalStorage;
    GridViewColumnHeader LvRecipesSortColumn;
    bool LvRecipesSortColumnAsc;
    Recipes Recipes;

    public ViewRecipes()
    {
      Recipes = new Recipes(Db);

      InitializeComponent();
      Refresh();
      Clear();
    }

    public void Refresh()
    {
      LvRecipes.ItemsSource = Recipes.Browse();
      CollectionViewSource.GetDefaultView(LvRecipes.ItemsSource).Refresh();
    }

    public void Clear()
    {
      BtnDelete.IsEnabled = false;
      BtnEdit.IsEnabled = false;
      BtnView.IsEnabled = false;
      BtnFavorite.IsEnabled = false;
      BtnUnfavorite.IsEnabled = false;
      BtnViewFavorite.IsEnabled = false;
    }

    private void BtnNewRecipe_Click(object sender, RoutedEventArgs e)
    {
      // Start with a blank Recipe
      Recipes.Current = new Recipe();
      FormRecipe formRecipe = new FormRecipe(Recipes);
      formRecipe.Owner = this;
      formRecipe.ShowDialog();
    }

    private void BtnEdit_Click(object sender, RoutedEventArgs e)
    {
      FormRecipe formRecipe = new FormRecipe(Recipes);
      formRecipe.Owner = this;
      formRecipe.ShowDialog();
    }

    private void BtnView_Click(object sender, RoutedEventArgs e)
    {
      ViewRecipe viewRecipe = new ViewRecipe(Recipes);
      viewRecipe.Owner = this;
      viewRecipe.ShowDialog();
    }

    private void MitQuit_Click(object sender, RoutedEventArgs e)
    {
      Close();
    }

    private void MitIngredients_Click(object sender, RoutedEventArgs e)
    {
      ViewIngredients viewIngredients = new ViewIngredients(Db);
      viewIngredients.ShowDialog();
    }

    private void LvRecipes_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (LvRecipes.SelectedItem is Recipe)
      {
        Recipes.Current = (Recipe)LvRecipes.SelectedItem;

        BtnEdit.IsEnabled = true;
        BtnDelete.IsEnabled = true;
        BtnView.IsEnabled = true;
        
        if (!FavoritesLocalStorage.IsFavorite(Recipes.Current))
        {
          BtnFavorite.IsEnabled = true;
        }
        else
        {
          BtnFavorite.IsEnabled = false;
        }
      }
    }

    private void GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
    {
      GridViewColumnHeader column = (sender as GridViewColumnHeader);
      string sortBy = column.Tag.ToString();
      ListSortDirection newDir = ListSortDirection.Ascending;

      // Clear existing sorts queued
      if (LvRecipesSortColumn != null)
      {
        LvRecipes.Items.SortDescriptions.Clear();
      }


      if (LvRecipesSortColumn == column && LvRecipesSortColumnAsc)
      {
        newDir = ListSortDirection.Descending;
        LvRecipesSortColumnAsc = false;
      }
      else
      {
        // Always default to ascending
        LvRecipesSortColumnAsc = true;
      }

      LvRecipesSortColumn = column;
      LvRecipes.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
    }

    private void BtnSearch_Click(object sender, RoutedEventArgs e)
    {
      Search search = new Search(Recipes);
      search.Owner = this;
      search.ShowDialog();
    }

    private void BtnDelete_Click(object sender, RoutedEventArgs e)
    {
      // Sanity check
      if (Recipes.Current != null)
      {
        MessageBoxResult result = MessageBox.Show(
          "Are you sure you want to delete this recipe?",
          "Confirm Delete",
          MessageBoxButton.YesNo,
          MessageBoxImage.Warning
        );

        if (result == MessageBoxResult.Yes)
        {
          bool success = Recipes.Delete(Recipes.Current);

          if (success)
          {
            // TODO: see similar TODO in ViewRecipe.xaml.cs
            Recipes.Current = null;
            MessageBox.Show(
              "Recipe deleted",
              "Success",
              MessageBoxButton.OK,
              MessageBoxImage.Information
            );

            Refresh();
            Clear();
          }
          else
          {
            MessageBox.Show(
              "Failed to delete Recipe",
              "Operation Failed",
              MessageBoxButton.OK,
              MessageBoxImage.Error
            );
          }
        }
      }
    }

    private void RunOfflineDataRoutine()
    {
      FavoritesLocalStorage = new FavoritesLocalStorage();
      
      if (
        FavoritesLocalStorage.MaybeLoad()
        && FavoritesLocalStorage.DeserialiseAndLoad(Recipes)
      )
      {
        FavoriteLoad.Text = FavoritesLocalStorage.FileURI;
        FavoriteStatus.Text = "Loaded at:";
      }
      else
      {
        FavoriteLoad.Text = FavoritesLocalStorage.FileURI;
        FavoriteStatus.Text = "Error loading:";
      }

      RefreshFavorites();

      // TODO: set status bar values
    }

    private void LbFavorites_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (LbFavorites.SelectedItem is Recipe)
      {
        FavoritesLocalStorage.Current = (Recipe)LbFavorites.SelectedItem;
        BtnUnfavorite.IsEnabled = true;
        BtnViewFavorite.IsEnabled = true;
      }
    }

    public void RefreshFavorites()
    {
      LbFavorites.ItemsSource = FavoritesLocalStorage.Favorites;
      CollectionViewSource.GetDefaultView(LbFavorites.ItemsSource).Refresh();
    }

    private void BtnFavorite_Click(object sender, RoutedEventArgs e)
    {
      FavoritesLocalStorage.Favorites.Add(Recipes.Current);
      BtnFavorite.IsEnabled = false;
      RefreshFavorites();
    }

    private void MitSave_Click(object sender, RoutedEventArgs e)
    {
      SaveFileDialog saveFileDialog = new SaveFileDialog();
      saveFileDialog.Filter = FavoritesLocalStorage.FileDialogFilter;
      saveFileDialog.FileName = FavoritesLocalStorage.DefaultFilename;
      saveFileDialog.Title = "Save favorite recipes as";
      saveFileDialog.ShowDialog();

      if (!string.IsNullOrWhiteSpace(saveFileDialog.FileName))
      {
        FavoritesLocalStorage.Close();
        FavoritesLocalStorage.MaybeCreateFile(saveFileDialog.FileName);
        FavoritesLocalStorage.Serialise();
        RefreshFavorites();

        FavoriteLoad.Text = FavoritesLocalStorage.FileURI;
        FavoriteStatus.Text = "Saved at:";
      }
    }

    private void MitLoad_Click(object sender, RoutedEventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = FavoritesLocalStorage.FileDialogFilter;
      openFileDialog.Title = "Load favorite recipes file";
      openFileDialog.ShowDialog();

      if (!string.IsNullOrWhiteSpace(openFileDialog.FileName))
      {
        FavoritesLocalStorage.Close();
        FavoritesLocalStorage.MaybeLoad(openFileDialog.FileName);
        FavoritesLocalStorage.DeserialiseAndLoad(Recipes);
        RefreshFavorites();

        FavoriteLoad.Text = FavoritesLocalStorage.FileURI;
        FavoriteStatus.Text = "Loaded at:";
      }

      // It's 04:45 AM, hours before deadline and I haven't done much testing yet
    }

    private void Window_Closing(object sender, CancelEventArgs e)
    {
      FavoritesLocalStorage.Serialise();
      FavoritesLocalStorage.Close();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      // Run offline data routine as WPF is not exposing window props on constructor
      RunOfflineDataRoutine();
    }

    private void BtnUnfavorite_Click(object sender, RoutedEventArgs e)
    {
      if (FavoritesLocalStorage.RemoveFromFavorites(FavoritesLocalStorage.Current))
      {
        LbFavorites.UnselectAll();
        BtnUnfavorite.IsEnabled = false;
        BtnViewFavorite.IsEnabled = false;
        RefreshFavorites();
      }
    }
  }
}
