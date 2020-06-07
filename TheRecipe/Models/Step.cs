namespace TheRecipe
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Step
    {
        public int Id { get; set; }

        [Column("Step")]
        [Required]
        [StringLength(255)]
        public string Content { get; set; }

        public byte? Order { get; set; }

        public int RecipeID { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
