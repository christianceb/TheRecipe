using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace TheRecipe.Class
{
  public class FavoritesLocalStorage
  {
    BinaryFormatter BFormatter = new BinaryFormatter();
    Stream FileStream;

    public List<Recipe> Favorites;
    public Recipe Current;
    public string FileURI
    {
      get;
      private set;
    }

    public string FileDialogFilter { get; private set; }
    public string DefaultFilename { get; private set; }
    string DefaultPath = "%userprofile%\\";
    string DefaultFileURI;

    public FavoritesLocalStorage ()
    {
      DefaultFilename = "recipes.dat";
      FileDialogFilter = "DAT file|*.dat";
      DefaultFileURI = Environment.ExpandEnvironmentVariables(DefaultPath + DefaultFilename);

      // You have no favorites boo 😂
      Favorites = new List<Recipe>();
    }

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

    public bool MaybeCreateFile(string targetFile = null)
    {
      bool success = false;
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

    public void Serialise()
    {
      List<int> FavoriteIds = new List<int>();

      foreach (Recipe recipe in Favorites)
      {
        FavoriteIds.Add(recipe.Id);
      }

      FileStream.SetLength(0);

      BFormatter.Serialize(FileStream, FavoriteIds);
    }

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
        /**
         * Deserialised stream might just be blank, or malformed. No harm in recreating it so
         * let's make way for the next serialization
         */
        MaybeCreateFile();
      }

      return success;
    }

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
