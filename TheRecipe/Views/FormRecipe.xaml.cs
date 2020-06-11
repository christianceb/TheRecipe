using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TheRecipe.Collections;

namespace TheRecipe
{
  /// <summary>
  /// Interaction logic for FormRecipe.xaml
  /// </summary>
  public partial class FormRecipe : Window
  {
    private readonly Recipes Recipes;
    private Ingredients ingredients;
    private Categories categories;
    private RecipeIngredients recipeIngredients;
    private Steps steps;
    private RecipesModel Db;
    private bool PendingChanges = false;

    public FormRecipe(Recipes recipes)
    {
      Recipes = recipes;

      if (recipes.Current == null)
      {
        recipes.Current = new Recipe();
      }

      DataContext = recipes.Current;

      InitializeComponent();

      // Default control states
      BtnSaveRecipe.IsEnabled = false;

      ClearNewRecipeIngredients();
      ClearNewIngredient();
      ClearSteps();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      // Find RecipesModel
      FindAndMaybeSetDb();

      // Load Ingredients collection
      ingredients = new Ingredients(Db);

      // Load Categories collection
      categories = new Categories(Db);

      // Load Recipe Ingredients collection
      recipeIngredients = new RecipeIngredients(Db);

      // Load Steps collection and set an empty step
      steps = new Steps(Db);

      // Ensure steps are loaded based on Order property
      LbSteps.DataContext = Recipes.Current.Steps.OrderBy(s => s.Order);

      // Refresh all controls that use these categories
      RefreshStep();
      RefreshIngredients();
      RefreshCategories();

      // Prevent false flag pending changes to recipe
      PendingChanges = false;
    }

    /// <summary>
    /// Set default state of Recipe Ingredients
    /// Deselect Recipe Ingredients Listbox and disable "Add to Recipe" button.
    /// </summary>
    private void ClearNewRecipeIngredients()
    {
      LbIngredients.UnselectAll();
      TbQuantity.Text = "";
      BtnAddRecipeIngredient.IsEnabled = false;
      BtnRemoveRecipeIngredient.IsEnabled = false;
    }

    /// <summary>
    /// Refresh Recipe Ingredients Listbox. ItemsSource must be changed for this to be effective
    /// </summary>
    private void RefreshRecipeIngredients()
    {
      CollectionViewSource.GetDefaultView(LvRecipeIngredients.ItemsSource).Refresh();
    }

    /// <summary>
    /// Set default state for Ingredients Lookup
    /// </summary>
    private void ClearNewIngredient()
    {
      TbIngredient.Text = "";
      BtnAddToLookup.IsEnabled = false;
    }

    /// <summary>
    /// Refresh ingredients list
    /// </summary>
    private void RefreshIngredients()
    {
      LbIngredients.DataContext = ingredients.Browse();
    }

    /// <summary>
    /// Refresh category fields. Binding automatically sets category if Recipe context has it set
    /// </summary>
    private void RefreshCategories()
    {
      CbCategory.ItemsSource = categories.Browse();
    }

    /// <summary>
    /// Set default state of steps
    /// </summary>
    private void ClearSteps()
    {
      TbStep.Text = "";

      TbStep.IsEnabled = false;
      BtnDeleteStep.IsEnabled = false;
      BtnSaveRecipe.IsEnabled = false;
      BtnUp.IsEnabled = false;
      BtnDown.IsEnabled = false;
      BtnSaveStep.IsEnabled = false;
      BtnCancelStep.IsEnabled = false;

      BtnNewStep.IsEnabled = true;
      LbSteps.IsEnabled = true;
    }

    /// <summary>
    /// Refresh step textbox based on value set in steps.Current
    /// </summary>
    private void RefreshStep()
    {
      //TbStep.DataContext = null;
      TbStep.DataContext = steps.Current;
    }

    /// <summary>
    /// Refreshes ItemSource of steps listbox
    /// </summary>
    private void RefreshSteps()
    {
      CollectionViewSource.GetDefaultView(LbSteps.ItemsSource).Refresh();
    }

    /// <summary>
    /// Recursively find in Windows.Owner property ViewRecipes
    /// </summary>
    /// <returns>The ViewRecipe if found, null otherwise</returns>
    private ViewRecipes FindViewRecipes()
    {
      bool end = false; // Flag if last possible iteration has been reached
      Window owner = Owner;

      while (end == false)
      {
        // Found class to look for
        if (owner is ViewRecipes)
        {
          return (ViewRecipes)owner;
        }
        else // Did not find class to look for
        {
          // If has owner
          if (owner.GetType().GetProperty("Owner") != null)
          {
            // Hoist owner.Owner to it owner
            owner = owner.Owner;
          }
          else
          {
            // End of windows to search for Db
            end = true;
          }
        }
      }

      return null;
    }

    /// <summary>
    /// Find a reference of RecipesModel and use it as Db on this window
    /// </summary>
    /// <returns>True if it found and set the Db reference. False otherwise</returns>
    private bool FindAndMaybeSetDb()
    {
      bool set = false;
      ViewRecipes viewRecipes = FindViewRecipes();

      if (viewRecipes != null)
      {
        Db = viewRecipes.Db;
        set = true;
      }

      return set;
    }

    /// <summary>
    /// Reorder this.Current to whatever direction it is set to be.
    /// </summary>
    /// <param name="up">If true, this.Current goes up. Down otherwise</param>
    /// <returns>True if successful, false otherwise especially if step has his either ceiling or floor of steps</returns>
    private bool ReorderSteps(bool up)
    {
      int low = 0;
      int high = LbSteps.Items.Count - 1;
      int selectedIndex = LbSteps.SelectedIndex;

      bool moved = false;

      Step
        target, // Target step to change places with, if available
        current = (Step)LbSteps.SelectedItem;

      // If up
      if (up)
      {
        // Only move current up if we're not in the ceiling
        if (low != selectedIndex)
        {
          // Get reference to previous object
          target = (Step)LbSteps.Items[selectedIndex - 1];
          target.Order++;
          current.Order--;

          moved = true;
        }
      }
      else // *satan voice* going down?
      {
        // Only move current down if we're not in the 9th circle of hell otherwise we go straight to Purgatorio, Dante.
        if (high != selectedIndex)
        {
          target = (Step)LbSteps.Items[selectedIndex + 1];
          target.Order--;
          current.Order++;

          moved = true;
        }
      }

      if (moved)
      {
        // Refresh list
        RefreshSteps();

        // Jump to where it moved
        LbSteps.ScrollIntoView(LbSteps.SelectedItem);

        ChangesMade();
      }

      return moved;
    }

    private void TbIngredient_TextChanged(object sender, TextChangedEventArgs e)
    {
      if (string.IsNullOrWhiteSpace(TbIngredient.Text))
      {
        BtnAddToLookup.IsEnabled = false;
      }
      else
      {
        BtnAddToLookup.IsEnabled = true;
      }
    }

    private void LbIngredients_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (LbIngredients.SelectedItem is Ingredient)
      {
        BtnAddRecipeIngredient.IsEnabled = true;
      }
    }

    private void BtnAddToLookup_Click(object sender, RoutedEventArgs e)
    {
      bool success = false;
      List<string> validateResults;

      ingredients.Ingredient = new Ingredient
      {
        Name = TbIngredient.Text
      };

      validateResults = ingredients.Validate(ingredients.Ingredient);

      if (validateResults.Count == 0)
      {
        success = ingredients.Add(ingredients.Ingredient);
      }

      if (success)
      {
        ClearNewIngredient();
        RefreshIngredients();
        LbIngredients.SelectedItem = ingredients.Ingredient;
        LbIngredients.ScrollIntoView(LbIngredients.SelectedItem);

        ;
      }
      else
      {
        MessageBox.Show("There are problems with your ingredient:\r\n" + string.Join("\r\n", validateResults.ToArray()), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    private void BtnAddRecipeIngredient_Click(object sender, RoutedEventArgs e)
    {
      bool success = false;
      List<string> validateResults;

      RecipeIngredient recipeIngredient = new RecipeIngredient()
      {
        Quantity = TbQuantity.Text,
        Recipe = Recipes.Current,
        Ingredient = (Ingredient)LbIngredients.SelectedItem
      };

      validateResults = recipeIngredients.Validate(recipeIngredient);

      if (validateResults.Count == 0)
      {
        success = true;
        Recipes.Current.RecipeIngredients.Add(recipeIngredient);
      }

      if (success)
      {
        ClearNewRecipeIngredients();
        RefreshRecipeIngredients();

        ChangesMade();

        // Jump to end of list
        LvRecipeIngredients.SelectedIndex = LvRecipeIngredients.Items.Count - 1;
        LvRecipeIngredients.ScrollIntoView(LvRecipeIngredients.SelectedItem);
      }
      else
      {
        MessageBox.Show("There are problems with your recipe ingredient:\r\n" + string.Join("\r\n", validateResults.ToArray()), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    private void BtnNewStep_Click(object sender, RoutedEventArgs e)
    {
      steps.Current = new Step();
      LbSteps.UnselectAll();
      RefreshStep();

      LbSteps.IsEnabled = false;
      TbStep.IsEnabled = true;
      BtnNewStep.IsEnabled = false;
      BtnDeleteStep.IsEnabled = false;
      BtnCancelStep.IsEnabled = true;

      BtnUp.IsEnabled = false;
      BtnDown.IsEnabled = false;

      // Focus on TbStep so user will know they can now edit the textbox
      TbStep.Focus();
    }

    private void BtnSaveStep_Click(object sender, RoutedEventArgs e)
    {
      bool success = false;
      int lastOrder = Recipes.FindLastOrder();
      List<string> validateResults;

      // Set order of new step
      steps.Current.Order = (byte)(lastOrder + 1);

      validateResults = steps.Validate(steps.Current);

      if (validateResults.Count == 0)
      {
        /**
         * Unlike ingredients, we can freely add items in Recipe.Steps because it is a list and
         * is not yet persisted.
         */
        Recipes.Current.Steps.Add(steps.Current);
        success = true;
      }

      if (success)
      {
        RefreshSteps();

        ClearSteps();
        RefreshStep();
        
        // Scroll to last
        LbSteps.ScrollIntoView(steps.Current);

        ChangesMade();
      }
      else
      {
        MessageBox.Show(
          "There are problems with your step:\r\n" + string.Join("\r\n", validateResults.ToArray()),
          "Error",
          MessageBoxButton.OK,
          MessageBoxImage.Error
        );
      }
    }

    /// <summary>
    /// A way to set a flag in the window if there are any pending changes in the Recipe
    /// </summary>
    private void ChangesMade()
    {
      // Only run if changes were made after window has finished loading.
      if (IsLoaded)
      {
        // Skip setting flag if already set
        if (!PendingChanges)
        {
          PendingChanges = true;
        }

        BtnSaveRecipe.IsEnabled = true;
      }
    }

    private void BtnBack_Click(object sender, RoutedEventArgs e)
    {
      Close(); // Closing routine will be taken care of in Window_Closing()
    }

    private void BtnRemoveRecipeIngredient_Click(object sender, RoutedEventArgs e)
    {
      if (LvRecipeIngredients.SelectedItem is RecipeIngredient)
      {
        Recipes.Current.RecipeIngredients.Remove(
          (RecipeIngredient)LvRecipeIngredients.SelectedItem
        );

        RefreshRecipeIngredients();

        BtnRemoveRecipeIngredient.IsEnabled = false;
        ChangesMade();
      }
    }

    private void BtnCancelStep_Click(object sender, RoutedEventArgs e)
    {
      // Reset step fields
      steps.Current = new Step();
      RefreshStep();

      ClearSteps();
    }

    private void LvRecipeIngredients_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      BtnRemoveRecipeIngredient.IsEnabled = true;
    }

    private void LbSteps_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (LbSteps.SelectedItem is Step)
      {
        steps.Current = (Step)LbSteps.SelectedItem;

        RefreshStep();

        BtnCancelStep.IsEnabled = true;
        TbStep.IsEnabled = true;
        BtnDeleteStep.IsEnabled = true;
        BtnUp.IsEnabled = true;
        BtnDown.IsEnabled = true;

        BtnNewStep.IsEnabled = false;
        BtnSaveStep.IsEnabled = false;
      }
    }

    private void TbStep_TextChanged(object sender, TextChangedEventArgs e)
    {
      // Automatically disable up/down to prevent further complexity
      BtnUp.IsEnabled = false;
      BtnDown.IsEnabled = false;

      if (!string.IsNullOrWhiteSpace(TbStep.Text))
      {
        BtnSaveStep.IsEnabled = true;
      }
      else
      {
        BtnSaveStep.IsEnabled = false;
      }
    }

    private void BtnUp_Click(object sender, RoutedEventArgs e)
    {
      // Sanity check
      if (LbSteps.SelectedItem is Step)
      {
        ReorderSteps(true);
      }
    }

    private void BtnDown_Click(object sender, RoutedEventArgs e)
    {
      // Sanity check
      if (LbSteps.SelectedItem is Step)
      {
        ReorderSteps(false);
      }
    }

    private void BtnSaveRecipe_Click(object sender, RoutedEventArgs e)
    {
      List<string> errors = Recipes.Validate(Recipes.Current);
      bool operationSuccess = false;

      if (errors.Count == 0)
      {
        // New Recipes always have 0 as their Id.
        if (Recipes.Current.Id == 0)
        {
          operationSuccess = Recipes.Add(Recipes.Current);
        }
        else // Existing recipes
        {
          operationSuccess = Recipes.Save();
        }

        if (operationSuccess)
        {
          MessageBox.Show(
            "Recipe saved!",
            "Success",
            MessageBoxButton.OK,
            MessageBoxImage.Information
          );
          
          // Turn off flag so we can close the window right away
          PendingChanges = false;

          // Update topmost parent window
          ViewRecipes viewRecipes = FindViewRecipes();
          if (viewRecipes != null)
          {
            viewRecipes.Refresh();
          }

          // TODO: also update owner if its not ViewRecipes

          Close();
        }
        else
        {
          MessageBox.Show(
            "Unexpected error saving your recipe",
            "Save failed",
            MessageBoxButton.OK,  
            MessageBoxImage.Error
          );
        }
      }
      else
      {
        MessageBox.Show(
          "There are problems saving your recipe:\r\n" + string.Join("\r\n", errors.ToArray()),
          "Error",
          MessageBoxButton.OK,
          MessageBoxImage.Error
        );
      }
    }

    private void TbTitle_TextChanged(object sender, TextChangedEventArgs e)
    {
      ChangesMade();
    }

    private void TbCost_TextChanged(object sender, TextChangedEventArgs e)
    {
      ChangesMade();
    }

    private void TbServings_TextChanged(object sender, TextChangedEventArgs e)
    {
      ChangesMade();
    }

    private void CbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      ChangesMade();
    }

    private void BtnDeleteStep_Click(object sender, RoutedEventArgs e)
    {
      Recipes.Current.Steps.Remove(steps.Current);
      RefreshSteps();
      ClearSteps();
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      // If user does not want to close, prevent closing of window
      if (!ClosingRoutine())
      {
        e.Cancel = true;
      }
    }

    /// <summary>
    /// Run window close routine.
    /// </summary>
    /// <returns>True if it is safe to close, false otherwise</returns>
    private bool ClosingRoutine()
    {
      bool continueClose = true;

      // Prompt if there were changes
      if (PendingChanges)
      {
        // Prompt user for unsaved changes
        MessageBoxResult result = MessageBox.Show(
          "You have unsaved changes. Discard them?",
          "Continue?",
          MessageBoxButton.YesNo,
          MessageBoxImage.Warning
        );

        // Discard and ensure Owner windows do not bear the unsaved changes
        if (result == MessageBoxResult.Yes)
        {
          // Discard changes and propagate to Owner windows
          Recipes.Discard();

          /**
           * There are only 2 known paths to this window:
           * 1. Via ViewRecipe
           * 2. Via ViewRecipes
           * 
           * #1 has code to iteratively find the topmost window and run refresh, but #2
           * still needs code to be traversed via iteration.
           * 
           * Since there are only 2 known paths, we will target the other path (ViewRecipe)
           * explicitly and not waste time writing an iterator to refresh every single
           * Owner.
           */

          // Find viewRecipes and reset data contexts and item sources based on discard
          ViewRecipes viewRecipes = FindViewRecipes();
          if (viewRecipes != null)
          {
            viewRecipes.Refresh();
          }

          // Explicitly refer to ViewRecipe if Owner is such and refresh it
          if (Owner is ViewRecipe)
          {
            ((ViewRecipe)Owner).Refresh();
          }
        }
        else // User does not want to lose changes
        {
          continueClose = false;
        }
      }

      return continueClose;
    }
  }
}
