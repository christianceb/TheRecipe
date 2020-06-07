namespace TheRecipe
{
  using System;
  using System.Data.Entity;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Linq;
    using System.Data.Entity.Validation;
    using System.Text;

    public partial class RecipesModel : DbContext
  {
    public RecipesModel()
        : base("name=TheRecipeModel")
    {
    }

    private void SaveChanges(DbContext context)
    {
      try
      {
        context.SaveChanges();
      }
      catch (DbEntityValidationException ex)
      {
        StringBuilder sb = new StringBuilder();

        foreach (var failure in ex.EntityValidationErrors)
        {
          sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
          foreach (var error in failure.ValidationErrors)
          {
            sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
            sb.AppendLine();
          }
        }

        throw new DbEntityValidationException(
            "Entity Validation Failed - errors follow:\n" +
            sb.ToString(), ex
        ); // Add the original exception as the innerException
      }
    }

    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Ingredient> Ingredients { get; set; }
    public virtual DbSet<RecipeIngredient> RecipeIngredients { get; set; }
    public virtual DbSet<Recipe> Recipes { get; set; }
    public virtual DbSet<Step> Steps { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Category>()
          .Property(e => e.Name)
          .IsUnicode(false);

      modelBuilder.Entity<Ingredient>()
          .Property(e => e.Name)
          .IsUnicode(false);

      modelBuilder.Entity<Ingredient>()
          .HasMany(e => e.RecipeIngredients)
          .WithRequired(e => e.Ingredient)
          .WillCascadeOnDelete(false);

      modelBuilder.Entity<Recipe>()
          .Property(e => e.Title)
          .IsUnicode(false);

      modelBuilder.Entity<Recipe>()
          .Property(e => e.Time)
          .IsUnicode(false);

      modelBuilder.Entity<Recipe>()
          .Property(e => e.Cost)
          .HasPrecision(18, 0);

      modelBuilder.Entity<RecipeIngredient>()
          .Property(e => e.Quantity)
          .IsUnicode(false);

      modelBuilder.Entity<Step>()
          .Property(e => e.Content)
          .IsUnicode(false);
    }
  }
}
