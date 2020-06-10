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
    Ingredients Ingredients;

    public ViewIngredients(RecipesModel db)
    {
      InitializeComponent();

      Ingredients = new Ingredients(db);

      Clear();
      Refresh();
    }

    public void Refresh()
    {
      //LbIngredients.DataContext = null;
      LbIngredients.DataContext = Ingredients.Browse();
    }

    public void Clear()
    {
      TbIngredient.Text = "";
      LbIngredients.UnselectAll();
      Ingredients.Current = null;
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
        Ingredients.Current = (Ingredient)LbIngredients.SelectedItem;
        TbIngredient.Text = Ingredients.Current.Name;
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
      if (Ingredients.Current == null)
      {
        Ingredient newIngredient = new Ingredient
        {
          Name = TbIngredient.Text
        };
        validateResults = Ingredients.Validate(newIngredient);

        if (validateResults.Count == 0)
        {
          success = Ingredients.Add(newIngredient);
        }
      }
      else
      {
        Ingredients.Current.Name = TbIngredient.Text;
        validateResults = Ingredients.Validate(Ingredients.Current);

        if (validateResults.Count == 0)
        {
          success = Ingredients.Edit(Ingredients.Current);
        }
      }

      if (success)
      {
        Clear();
        Refresh();

        MessageBox.Show(
          "Ingredient added!",
          "Success",
          MessageBoxButton.OK,
          MessageBoxImage.Information
        );
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
      if (Ingredients.Current != null)
      {
        if (Ingredients.Delete(Ingredients.Current))
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
