using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace TheRecipe
{
  public class FavoriteLocalStorage
  {
    readonly BinaryFormatter BFormatter = new BinaryFormatter();
    Stream FileStream;

    public Recipe Current;
    public ERecipe ECurrent;

    public string FileURI { get; private set; }
    public string FileDialogFilter { get; private set; }
    public string Error { get; private set; }

    public FavoriteLocalStorage()
    {
      FileDialogFilter = "DAT file|*.dat";
    }

    public void Close()
    {
      FileStream.Close();
    }

    public bool MaybeLoad(string targetFile = null)
    {
      bool success = false;

      try
      {
        FileStream = File.Open(targetFile, FileMode.Open);
        success = true;
      }
      catch (Exception exception)
      {
        Error = exception.ToString();
        // TODO: catch other exceptions
        Console.WriteLine(exception);
      }

      return success;
    }

    public bool MaybeCreateFile(string targetFile = null)
    {
      bool success = false;

      try
      {
        FileStream = File.Open(targetFile, FileMode.OpenOrCreate);
        success = true;
      }
      catch (Exception exception)
      {
        Error = exception.ToString();

        // TODO: catch other exceptions
        Console.WriteLine(exception);
      }

      return success;
    }

    public bool Serialise()
    {
      bool success = false;

      if (Current != null && FileStream != null)
      {
        // Always clear existing file stream to avoid stacking binary formatted data
        FileStream.SetLength(0);
        Convert();
        BFormatter.Serialize(FileStream, ECurrent);
      }
      else
      {
        Error = "Recipe or filestream not set";
      }

      return success;
    }

    public bool Deserialize()
    {
      bool success = false;

      if (FileStream != null)
      {

        try
        {
          ECurrent = (ERecipe)BFormatter.Deserialize(FileStream);

          success = true;
        }
        catch (SerializationException exception)
        {
          Error = "Invalid file contents";
        }
        catch (Exception exception)
        {
          Error = exception.ToString();
        }
      }
      else
      {
        Error = "No open filestream";
      }


      return success;
    }

    public bool Convert()
    {
      bool success = false;

      if (FileStream != null && Current != null)
      {
        ECurrent = ConvertRecipe(Current);
      }
      else
      {
        Error = "Recipe or filestream not set";
      }

      return success;
    }

    public ERecipe ConvertRecipe(Recipe recipe)
    {
      ERecipe converted = new ERecipe()
      {
        Id = recipe.Id,
        Title = recipe.Title,
        Time = recipe.Time,
        Cost = recipe.Cost,
        Servings = recipe.Servings,
        CategoryId = recipe.CategoryId,
        Category = new ECategory()
        {
          Id = recipe.Category.Id,
          Name = recipe.Category.Name
        }
      };

      List<ERecipeIngredient> recipeIngredients = new List<ERecipeIngredient>();
      foreach (RecipeIngredient item in recipe.RecipeIngredients)
      {
        recipeIngredients.Add(new ERecipeIngredient()
        {
          Id = item.Id,
          Ingredient = new EIngredient()
          {
            Id = item.Ingredient.Id,
            Name = item.Ingredient.Name
          },
          Quantity = item.Quantity,
          RecipeID = item.RecipeID,
          IngredientID = item.IngredientID
        });
      }

      converted.RecipeIngredients = recipeIngredients;

      List<EStep> steps = new List<EStep>();
      foreach (Step item in recipe.Steps)
      {
        steps.Add(new EStep()
        {
          Id = item.Id,
          Content = item.Content,
          Order = item.Order,
          RecipeID = item.RecipeID
        });
      }

      converted.Steps = steps;

      return converted;
    }
  }
}
