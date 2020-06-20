using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TheRecipe
{
  [Serializable]
  public class ERecipe : ISerializable
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public int? Time { get; set; }
    public decimal? Cost { get; set; }
    public byte? Servings { get; set; }
    public int? CategoryId { get; set; }
    public virtual ECategory Category { get; set; }
    public virtual List<ERecipeIngredient> RecipeIngredients { get; set; }
    public virtual List<EStep> Steps { get; set; }

    public ERecipe() { }

    public ERecipe(SerializationInfo info, StreamingContext context)
    {
      Id = (int)info.GetValue("Id", typeof(int));
      Title = (string)info.GetValue("Title", typeof(string));
      Time = (int)info.GetValue("Time", typeof(int));
      Cost = (decimal)info.GetValue("Cost", typeof(decimal));
      Servings = (byte)info.GetValue("Servings", typeof(byte));
      CategoryId = (int)info.GetValue("CategoryId", typeof(int));

      Category = (ECategory)info.GetValue("Category", typeof(ECategory));
      RecipeIngredients = (List<ERecipeIngredient>)info.GetValue("RecipeIngredients", typeof(List<ERecipeIngredient>));
      Steps = (List<EStep>)info.GetValue("Steps", typeof(List<EStep>));
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      info.AddValue("Id", Id);
      info.AddValue("Title", Title);
      info.AddValue("Time", Time);
      info.AddValue("Cost", Cost);
      info.AddValue("Servings", Servings);
      info.AddValue("CategoryId", CategoryId);

      info.AddValue("Category", Category);
      info.AddValue("RecipeIngredients", RecipeIngredients);
      info.AddValue("Steps", Steps);
    }
  }
}
