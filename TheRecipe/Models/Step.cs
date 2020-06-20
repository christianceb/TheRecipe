namespace TheRecipe
{
  using System;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Runtime.Serialization;

  public partial class Step
  {
    public int Id { get; set; }

    [Column(TypeName = "Text")]
    [Required]
    public string Content { get; set; }

    public byte? Order { get; set; }

    public int RecipeID { get; set; }

    public virtual Recipe Recipe { get; set; }
  }
}
