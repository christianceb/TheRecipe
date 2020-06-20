namespace TheRecipe
{
  using System;
  using System.ComponentModel.DataAnnotations;
  using System.Runtime.Serialization;

  [Serializable]
  public partial class ERecipeIngredient : ISerializable
  {
    public int Id { get; set; }
    public int RecipeID { get; set; }
    public int IngredientID { get; set; }
    public string Quantity { get; set; }
    public virtual EIngredient Ingredient { get; set; }

    public ERecipeIngredient() { }

    public ERecipeIngredient(SerializationInfo info, StreamingContext context)
    {
      Id = (int)info.GetValue("Id", typeof(int));
      RecipeID = (int)info.GetValue("RecipeID", typeof(int));
      IngredientID = (int)info.GetValue("IngredientID", typeof(int));
      Quantity = (string)info.GetValue("Quantity", typeof(string));

      Ingredient = (EIngredient)info.GetValue("Ingredient", typeof(EIngredient));
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      info.AddValue("Id", Id);
      info.AddValue("RecipeID", RecipeID);
      info.AddValue("IngredientID", IngredientID);
      info.AddValue("Quantity", Quantity);

      info.AddValue("Ingredient", Ingredient);
    }
  }
}
