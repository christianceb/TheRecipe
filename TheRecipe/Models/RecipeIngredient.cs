namespace TheRecipe
{
  using System;
  using System.ComponentModel.DataAnnotations;
  using System.Runtime.Serialization;

  public partial class RecipeIngredient
  {
    public int Id { get; set; }

    public int RecipeID { get; set; }

    public int IngredientID { get; set; }

    [StringLength(50)]
    public string Quantity { get; set; }

    public virtual Ingredient Ingredient { get; set; }

    public virtual Recipe Recipe { get; set; }
  }
}
