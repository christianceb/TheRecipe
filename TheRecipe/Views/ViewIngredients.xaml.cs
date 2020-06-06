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
using System.Windows.Shapes;

namespace TheRecipe
{
  /// <summary>
  /// Interaction logic for ViewIngredients.xaml
  /// </summary>
  public partial class ViewIngredients : Window
  {
    Ingredients ingredients;
    Ingredient selectedIngredient;

    public ViewIngredients(RecipesModel db)
    {
      InitializeComponent();

      ingredients = new Ingredients(db);

      Clear();
      Refresh();
    }

    public void Refresh()
    {
      LbIngredients.DataContext = null;
      LbIngredients.DataContext = ingredients.Browse();
    }

    public void Clear()
    {
      TbIngredient.Text = "";
      LbIngredients.UnselectAll();
      selectedIngredient = null;
      BtnSave.Content = "Add";
      BtnDelete.IsEnabled = false;
      BtnClear.IsEnabled = false;
    }

    private void BtnBack_Click(object sender, RoutedEventArgs e)
    {
      Close();
    }

    private void LbIngredients_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (LbIngredients.SelectedItem is Ingredient)
      {
        selectedIngredient = (Ingredient)LbIngredients.SelectedItem;
        TbIngredient.Text = selectedIngredient.Name;
        BtnSave.Content = "Save";

        BtnClear.IsEnabled = true;
        BtnDelete.IsEnabled = true;
      }
    }

    private void BtnSave_Click(object sender, RoutedEventArgs e)
    {
      bool success = false;
      List<string> validateResults;

      // Add ingredient
      if (selectedIngredient == null)
      {
        Ingredient newIngredient = new Ingredient
        {
          Name = TbIngredient.Text
        };
        validateResults = ingredients.Validate(newIngredient);

        if (validateResults.Count == 0)
        {
          success = ingredients.Add(newIngredient);
        }
      }
      else
      {
        selectedIngredient.Name = TbIngredient.Text;
        validateResults = ingredients.Validate(selectedIngredient);

        if (validateResults.Count == 0)
        {
          success = ingredients.Edit(selectedIngredient);
        }
      }

      if (success)
      {
        Clear();
        Refresh();
      }
      else
      {
        MessageBox.Show("There are problems with your ingredient:\r\n" + string.Join("\r\n", validateResults.ToArray()), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    private void BtnClear_Click(object sender, RoutedEventArgs e)
    {
      Clear();
    }

    private void BtnDelete_Click(object sender, RoutedEventArgs e)
    {
      if (selectedIngredient != null)
      {
        if (ingredients.Delete(selectedIngredient))
        {
          Clear();
          Refresh();
        }
        else
        {
          MessageBox.Show("There was a problem deleting this ingredient. Try again later.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
      }
    }
  }
}
