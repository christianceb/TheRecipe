﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRecipe
{
  public class Recipes : Collection<Recipe>
  {
    public Recipes (RecipesModel recipesModel)
    {
      Db = recipesModel;

      RefreshList();
    }

    public void RefreshList()
    {
      List = Db.Recipes.OrderByDescending(r => r.Id).ToList();
    }

    public override bool Add(Recipe Recipe)
    {
      bool success = false;

      using (DbContextTransaction transaction = Db.Database.BeginTransaction())
      {
        try
        {
          Db.Recipes.Add(Recipe);
          Db.SaveChanges();
          transaction.Commit();

          RefreshList();
          success = true;
        }
        catch (Exception exception)
        {
          transaction.Rollback();

          // Should you need to explain what the exception was
          _ = exception.Message;
        }
      }

      return success;
    }

    public override List<Recipe> Browse()
    {
      if (Db.Recipes.Count() != List.Count)
      {
        RefreshList();
      }

      return List;
    }

    public override bool Delete(Recipe item)
    {
      // TODO: false state and convert to txn
      Db.Recipes.Remove(item);
      Db.SaveChanges();

      return true;
    }

    public override bool Delete(int id)
    {
      throw new NotImplementedException();
    }

    public override bool Edit(Recipe item)
    {
      throw new NotImplementedException();
    }

    public override void Read(int id)
    {
      throw new NotImplementedException();
    }

    public override List<string> Validate(Recipe Recipe)
    {
      List<string> errors = new List<string>();

      if (string.IsNullOrEmpty(Recipe.Title))
      {
        errors.Add("Title is blank");
      }

      if (Recipe.Servings <= 0)
      {
        errors.Add("Invalid servings entered");
      }

      if (Recipe.Cost <= 0)
      {
        errors.Add("Invalid cost entered");
      }

      // Enforce greater than 0 constraint to time
      if (Recipe.Time < 0)
      {
        errors.Add("Invalid time entered");
      }

      return errors;
    }

    public int FindLastOrder(Recipe recipe = null)
    {
      Recipe searchRecipe;
      int last = 0;

      if (recipe == null)
      {
        searchRecipe = Current;
      }
      else
      {
        searchRecipe = recipe;
      }

      if (searchRecipe.Steps.Count > 1)
      {
        last = searchRecipe.Steps.Count - 1;
      }

      return last;
    }
  }
}
