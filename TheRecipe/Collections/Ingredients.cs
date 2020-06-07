﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRecipe
{
  public class Ingredients : Collection<Ingredient>
  {
    public Ingredients(RecipesModel recipesModel)
    {
      Db = recipesModel;

      RefreshList();
    }

    public override bool Add(Ingredient ingredient)
    {
      try
      {
        Db.Ingredients.Add(ingredient);
        Db.SaveChanges();
        RefreshList();
      } catch (Exception exception)
      {
        // TODO: gracefully handle exception maybe?
        Console.WriteLine(exception);
      }

      return true;
    }

    public List<string> Validate(Ingredient ingredient)
    {
      List<string> errors = new List<string>();

      if ( String.IsNullOrEmpty(ingredient.Name) )
      {
        errors.Add("Name is blank");
      }

      // Ensure name follows constraints
      if (Db.Ingredients.Where(i=>i.Name == ingredient.Name).Count() > 0)
      {
        errors.Add("Ingredient already exists");
      }

      return errors;
    }

    public void RefreshList()
    {
      List = Db.Ingredients.OrderBy(i => i.Name).ToList();
    }

    public override List<Ingredient> Browse()
    {
      if ( Db.Ingredients.Count() != List.Count )
      {
        RefreshList();
      }

      return List;
    }

    // public ObservableCollection<Ingredient> Local => Db.Ingredients.Local;

    public override bool Edit(Ingredient item)
    {
      Ingredient ingredient = Db.Ingredients.Find(item.Id);
      
      if (ingredient == null)
      {
        return false;
      }

      ingredient = item;
      Db.SaveChanges();

      return true;
    }

    public override void Read(int id)
    {
      throw new NotImplementedException();
    }

    public override bool Delete(Ingredient ingredient)
    {
      // TODO: false state;

      Db.Ingredients.Remove(ingredient);
      Db.SaveChanges();
      return true;
    }

    public override bool Delete(int id)
    {
      Ingredient ingredient = Db.Ingredients.Where(i => i.Id == 4).FirstOrDefault();

      if (ingredient == null)
      {
        return false;
      }

      return Delete(ingredient);
    }
  }
}