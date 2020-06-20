using System;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace TheRecipe
{
  /// <summary>
  /// Interaction logic for ViewRecipe.xaml
  /// </summary>
  public partial class ViewRecipe : Window
  {
    private Recipes Recipes;
    private FavoriteLocalStorage FavoriteLocalStorage = new FavoriteLocalStorage();

    public ViewRecipe(Recipes recipes)
    {
      Recipes = recipes;
      DataContext = Recipes.Current;

      InitializeComponent();
    }

    private void BtnBack_Click(object sender, RoutedEventArgs e)
    {
      Close();
    }

    private void BtnDelete_Click(object sender, RoutedEventArgs e)
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
          // TODO: if you can omit passing a recipe in Delete, the better so you can omit the line below too
          // TODO: i don't remember what the TODO above is anymore but whatevs.
          Recipes.Current = null;
          MessageBox.Show(
            "Recipe deleted",
            "Success",
            MessageBoxButton.OK,
            MessageBoxImage.Information
          );

          ((ViewRecipes)Owner).Refresh();
          ((ViewRecipes)Owner).Clear();

          Close();
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

    private void BtnEdit_Click(object sender, RoutedEventArgs e)
    {
      FormRecipe formRecipe = new FormRecipe(Recipes)
      {
        Owner = this
      };
      formRecipe.ShowDialog();
    }

    internal void Refresh()
    {
      DataContext = Recipes.Current;
    }

    private void BtnSave_Click(object sender, RoutedEventArgs e)
    {
      SaveFileDialog saveFileDialog = new SaveFileDialog();
      saveFileDialog.Filter = FavoriteLocalStorage.FileDialogFilter;
      saveFileDialog.Title = "Save favorite recipe as";
      saveFileDialog.ShowDialog();

      if (!string.IsNullOrWhiteSpace(saveFileDialog.FileName))
      {
        if (FavoriteLocalStorage.MaybeCreateFile(saveFileDialog.FileName))
        {
          FavoriteLocalStorage.Current = Recipes.Current;
          FavoriteLocalStorage.Serialise();
          FavoriteLocalStorage.Close();

          MessageBox.Show(
            "Recipe saved!",
            "Success",
            MessageBoxButton.OK,
            MessageBoxImage.Information
          );
        }
      }
    }
  }
}
