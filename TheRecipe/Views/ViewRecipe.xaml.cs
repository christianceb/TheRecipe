using System.Windows;

namespace TheRecipe
{
  /// <summary>
  /// Interaction logic for ViewRecipe.xaml
  /// </summary>
  public partial class ViewRecipe : Window
  {
    private Recipes Recipes;

    public ViewRecipe(Recipes recipes)
    {
      Recipes = recipes;
      DataContext = recipes.Current;

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
          Recipes.Current = null;
          MessageBox.Show(
            "Recipe deleted",
            "Success",
            MessageBoxButton.OK,
            MessageBoxImage.Information
          );

          ((ViewRecipes)Owner).Refresh();

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
  }
}
