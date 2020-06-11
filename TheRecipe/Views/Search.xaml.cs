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
  /// Interaction logic for Search.xaml
  /// </summary>
  public partial class Search : Window
  {
    Recipes Recipes;
    public List<Recipe> Results;

    public Search(Recipes recipes)
    {
      Recipes = recipes;

      InitializeComponent();
      SetDefaultSearchOptions();
      ClearSearch();
      RefreshResults();
    }

    private void ClearSearch()
    {
      BtnView.IsEnabled = false;
      TbKeywords.Text = "";
      LbResults.UnselectAll();
    }

    private void RefreshResults()
    {
      if (Results == null)
      {
        Results = new List<Recipe>();
      }

      LbResults.ItemsSource = Results;
      CollectionViewSource.GetDefaultView(LbResults.ItemsSource).Refresh();
    }

    private void SetDefaultSearchOptions()
    {
      List<SearchOptions> lbSearchOptions = new List<SearchOptions>();

      lbSearchOptions.Add(new SearchOptions()
      {
        Value = "containsKeywords",
        Name = "Contains keywords"
      });

      lbSearchOptions.Add(new SearchOptions()
      {
        Value = "containsIngredients",
        Name = "Contains ingredients"
      });

      lbSearchOptions.Add(new SearchOptions()
      {
        Value = "withinTime",
        Name = "Less than the specified time"
      });

      CbSearchOptions.ItemsSource = lbSearchOptions;
    }

    private void BtnBack_Click(object sender, RoutedEventArgs e)
    {
      Close();
    }

    private void BtnSearch_Click(object sender, RoutedEventArgs e)
    {
      string searchOption;
      string searchKeywords = TbKeywords.Text;
      bool searchValid = false, hasFiltered = false;

      if (CbSearchOptions.SelectedItem is SearchOptions && !string.IsNullOrWhiteSpace(searchKeywords))
      {
        searchValid = true;
        searchOption = ((SearchOptions)CbSearchOptions.SelectedItem).Value;

        switch (searchOption)
        {
          case "containsKeywords":
            hasFiltered = FilterKeywords(searchKeywords); 
            break;
          case "containsIngredients":
            hasFiltered = FilterIngredients(searchKeywords);
            break;
          case "withinTime":
            hasFiltered = FilterTime(searchKeywords);
            break;
          default:
            break;
        }
      }

      if (!searchValid)
      {
        MessageBox.Show(
          "Complete search fields to start a search",
          "Missing details",
          MessageBoxButton.OK,
          MessageBoxImage.Warning
        );
      }
      else if (!hasFiltered)
      {
        MessageBox.Show(
          "There was an error searching. Try again later.",
          "Error",
          MessageBoxButton.OK,
          MessageBoxImage.Error
        );
      }
      else
      {
        RefreshResults();
      }
    }

    private bool FilterTime(string time)
    {
      bool success = false;
      int minutes;

      if (int.TryParse(time, out minutes))
      {
        Results = Recipes.SearchLessThanTime(minutes);
        success = true;
      }

      return success;
    }

    private bool FilterKeywords(string keywords)
    {
      bool success = false;

      // Nonsense blocking but we might as well CYA
      try
      {
        Results = Recipes.SearchByKeyword(keywords);
        success = true;
      }
      catch (Exception)
      {
        /**
         * List of things to do here:
         * - absolutely nothing
         */
      }

      // Permanently true unless you add more complexity to this method
      return success;
    }

    private bool FilterIngredients(string keywords)
    {
      bool success = false;

      // Nonsense blocking but we might as well CYA
      try
      {
        Results = Recipes.SearchRecipesContainingIngredients(keywords);
        success = true;
      }
      catch (Exception)
      {
        /**
         * List of things to do here:
         * - absolutely nothing
         */
      }

      // Permanently true unless you add more complexity to this method
      return success;
    }

    private void BtnClear_Click(object sender, RoutedEventArgs e)
    {
      Results = new List<Recipe>();
      ClearSearch();
      RefreshResults();
    }

    private void LbResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (LbResults.SelectedItem is Recipe)
      {
        BtnView.IsEnabled = true;
      }
    }

    private void BtnView_Click(object sender, RoutedEventArgs e)
    {
      Recipes.Current = (Recipe)LbResults.SelectedItem;
      Close();

      ViewRecipe viewRecipe = new ViewRecipe(Recipes);
      viewRecipe.Owner = Owner;
      viewRecipe.ShowDialog();
    }
  }

  /// <summary>
  /// To be used in search options (CbSearchOptions)
  /// </summary>
  public class SearchOptions
  {
    public string Value { get; set; }
    public string Name { get; set; }
  }
}
