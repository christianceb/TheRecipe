using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace TheRecipe
{
  /// <summary>
  /// Class for handling local storage and binary formatting under context of Favorites
  /// </summary>
  public class FavoritesLocalStorage
  {
    readonly BinaryFormatter BFormatter = new BinaryFormatter();
    Stream FileStream;

    public List<Recipe> Favorites;
    public Recipe Current;
    public string FileURI { get; private set; }
    public string FileDialogFilter { get; private set; }
    public string DefaultFilename { get; private set; }
    readonly string DefaultPath = "%userprofile%\\";
    readonly string DefaultFileURI;

    public FavoritesLocalStorage()
    {
      // Set default local storage parameters
      DefaultFilename = "recipes.dat";
      FileDialogFilter = "DAT file|*.dat";
      DefaultFileURI = Environment.ExpandEnvironmentVariables(DefaultPath + DefaultFilename);

      // You have no favorites boo 😂
      Favorites = new List<Recipe>();
    }

    /// <summary>
    /// Load a given URI and set it as the file stream. Not to be confused with MaybeCreateFile() that has different parameters in opening a file.
    /// </summary>
    /// <param name="targetFile">URI of file</param>
    /// <returns>True if successful, false otherwise</returns>
    public bool MaybeLoad(string targetFile = null)
    {
      bool success = false;

      FileURI = targetFile ?? DefaultFileURI;

      try
      {
        FileStream = File.Open(FileURI, FileMode.OpenOrCreate);
        success = true;
      }
      catch (Exception)
      {
        // Do nothing for now
      }

      return success;
    }

    /// <summary>
    /// Try to create AND open target file
    /// </summary>
    /// <param name="targetFile">URI of file</param>
    /// <returns>True if successful, false otherwise</returns>
    public bool MaybeCreateFile(string targetFile = null)
    {
      bool success = false;
      
      FileURI = targetFile ?? DefaultFileURI;

      try
      {
        FileStream = File.Open(targetFile ?? DefaultFileURI, FileMode.Create);
        success = true;
      }
      catch (Exception)
      {
        // Do nothing for now
      }

      return success;
    }

    /// <summary>
    /// Collate recipes into Recipe Ids and serializes it to file
    /// </summary>
    public void Serialise()
    {
      List<int> FavoriteIds = new List<int>();
      foreach (Recipe recipe in Favorites)
      {
        FavoriteIds.Add(recipe.Id);
      }

      // Always clear existing file stream to avoid stacking binary formatted data
      FileStream.SetLength(0);

      BFormatter.Serialize(FileStream, FavoriteIds);
    }

    /// <summary>
    /// Deserialize file in stream. If it is digestible (a list of integers), use the integers as Recipe IDs and load them
    /// </summary>
    /// <param name="recipes">The recipes data collection object</param>
    /// <returns>True if successful, false otherwise</returns>
    public bool DeserialiseAndLoad(Recipes recipes)
    {
      bool success = false;

      try
      {
        List<int> FavoriteIds = (List<int>)BFormatter.Deserialize(FileStream);
        Favorites = recipes.Read(FavoriteIds);
        success = true;
      }
      catch (Exception exception)
      {
        // It could be throwing an exception only because it's an empty file. If then, consider it a success
        if (FileStream.Length == 0)
        {
          success = true;
        }
        else // Otherwise, let's just recreate it and empty it
        {
          Close();
          MaybeCreateFile();

          Console.WriteLine(exception.ToString());
        }
      }

      return success;
    }

    /// <summary>
    /// Adds a recipe to favorites. Also ensures it is not erroneous
    /// </summary>
    /// <param name="recipe">Recipe to add</param>
    /// <returns>True if successful, false otherwise</returns>
    public bool AddToFavorites(Recipe recipe)
    {
      bool success = false;

      if (!IsFavorite(recipe))
      {
        Favorites.Add(recipe);
        success = true;
      }

      return success;
    }

    /// <summary>
    /// Remove a recipe from favorites. Also ensures it is not erroneous
    /// </summary>
    /// <param name="recipe">Recipe to remove</param>
    /// <returns>True if successful, false otherwise</returns>
    public bool RemoveFromFavorites(Recipe recipe)
    {
      bool success = false;

      if (IsFavorite(recipe))
      {
        Favorites.Remove(recipe);
        success = true;
      }

      return success;

      // It's already 05:23 AM and I am well deep into going against DRY
    }

    /// <summary>
    /// Finds a given recipe in the favorites list. If found, it is a favorite.
    /// </summary>
    /// <param name="recipe">Recipe to match</param>
    /// <returns>True if it is a favorite, false otherwise.</returns>
    public bool IsFavorite(Recipe recipe)
    {
      bool favorite = false;

      if (Favorites.Exists(f => f.Id == recipe.Id))
      {
        favorite = true;
      }

      return favorite;
    }

    public void Close()
    {
      FileStream.Close();
    }
  }
}
