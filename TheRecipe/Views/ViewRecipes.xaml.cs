using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace TheRecipe
{
  /// <summary>
  /// Interaction logic for ViewRecipes.xaml
  /// </summary>
  public partial class ViewRecipes : Window
  {
    public RecipesModel Db = new RecipesModel();
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
        BtnFavorite.IsEnabled = true;
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
  }
}
