namespace TheRecipe
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RecipeIngredient
    {
        public int Id { get; set; }

        public int RecipeID { get; set; }

        public int IngredientID { get; set; }

        public virtual Ingredient Ingredient { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
