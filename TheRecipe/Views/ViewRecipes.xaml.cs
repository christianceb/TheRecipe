using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TheRecipe
{
  /// <summary>
  /// Interaction logic for ViewRecipes.xaml
  /// </summary>
  public partial class ViewRecipes : Window
  {
    RecipesModel Db = new RecipesModel();

    public ViewRecipes()
    {
      InitializeComponent();
    }

    private void btnNewRecipe_Click(object sender, RoutedEventArgs e)
    {
      FormRecipe formRecipe = new FormRecipe();
      formRecipe.ShowDialog();
    }

    private void btnEdit_Click(object sender, RoutedEventArgs e)
    {
      FormRecipe formRecipe = new FormRecipe();
      formRecipe.ShowDialog();
    }

    private void btnView_Click(object sender, RoutedEventArgs e)
    {
      ViewRecipe viewRecipe = new ViewRecipe();
      viewRecipe.ShowDialog();
    }

    private void mitQuit_Click(object sender, RoutedEventArgs e)
    {
      Close();
    }

    private void mitIngredients_Click(object sender, RoutedEventArgs e)
    {
      ViewIngredients viewIngredients = new ViewIngredients(Db);
      viewIngredients.ShowDialog();
    }
  }
}
