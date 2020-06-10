using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRecipe.Collections
{
  class RecipeIngredients : Collection<RecipeIngredient>
  {
    public RecipeIngredients(RecipesModel db)
    {
      Db = db;
    }

    public override bool Add(RecipeIngredient item)
    {
      throw new NotImplementedException();
    }

    public override List<RecipeIngredient> Browse()
    {
      throw new NotImplementedException();
    }

    public override bool Delete(RecipeIngredient item)
    {
      throw new NotImplementedException();
    }

    public override bool Delete(int id)
    {
      throw new NotImplementedException();
    }

    public override bool Edit(RecipeIngredient item)
    {
      throw new NotImplementedException();
    }

    public override void Read(int id)
    {
      throw new NotImplementedException();
    }

    public override List<string> Validate(RecipeIngredient recipeIngredient)
    {
      List<string> errors = new List<string>();

      if (recipeIngredient.Ingredient == null)
      {
        errors.Add("Ingredient not set");
      }

      if (string.IsNullOrWhiteSpace(recipeIngredient.Quantity))
      {
        errors.Add("Quantity is blank");
      }

      // TODO: how to validate if recipe wasn't saved yet?
      /*if (recipeIngredient.Recipe == null)
      {
        errors.Add("Ingredient not set");
      }*/

      return errors;
    }
  }
}
