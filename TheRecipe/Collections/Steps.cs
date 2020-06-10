using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRecipe.Collections
{
  class Steps : Collection<Step>
  {
    public Steps(RecipesModel recipesModel)
    {
      Db = recipesModel;
    }

    public override bool Add(Step item)
    {
      throw new NotImplementedException();
    }

    public override List<Step> Browse()
    {
      throw new NotImplementedException();
    }

    public override bool Delete(Step item)
    {
      throw new NotImplementedException();
    }

    public override bool Delete(int id)
    {
      throw new NotImplementedException();
    }

    public override bool Edit(Step item)
    {
      throw new NotImplementedException();
    }

    public override void Read(int id)
    {
      throw new NotImplementedException();
    }

    public override List<string> Validate(Step step)
    {
      List<string> errors = new List<string>();

      if (string.IsNullOrWhiteSpace(step.Content))
      {
        errors.Add("Step is empty!");
      }

      // TODO: maybe ensure that the steps are properly ordered?

      return errors;
    }
  }
}
