using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRecipe.Collections
{
  class Categories : Collection<Category>
  {
    public Categories(RecipesModel recipesModel)
    {
      Db = recipesModel;

      RefreshList();
    }

    public override bool Add(Category item)
    {
      throw new NotImplementedException();
    }

    public override List<Category> Browse()
    {
      if (Db.Categories.Count() != List.Count)
      {
        RefreshList();
      }

      return List;
    }

    public void RefreshList()
    {
      List = Db.Categories.OrderBy(i => i.Name).ToList();
    }

    public override bool Delete(Category item)
    {
      throw new NotImplementedException();
    }

    public override bool Delete(int id)
    {
      throw new NotImplementedException();
    }

    public override bool Edit(Category item)
    {
      throw new NotImplementedException();
    }

    public override void Read(int id)
    {
      throw new NotImplementedException();
    }

    public override List<string> Validate(Category item)
    {
      throw new NotImplementedException();
    }
  }
}
