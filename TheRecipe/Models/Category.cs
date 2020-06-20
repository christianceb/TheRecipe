namespace TheRecipe
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.Runtime.Serialization;

  public partial class Category
  {
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Category()
    {
      Recipes = new HashSet<Recipe>();
    }

    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual System.Collections.Generic.ICollection<Recipe> Recipes { get; set; }

  }
}
