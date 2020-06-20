namespace TheRecipe
{
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;

  public partial class Ingredient
  {
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Ingredient()
    {
      RecipeIngredients = new HashSet<RecipeIngredient>();
    }

    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual System.Collections.Generic.ICollection<RecipeIngredient> RecipeIngredients { get; set; }
  }
}
