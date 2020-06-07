namespace TheRecipe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50, unicode: false),
                        Time = c.String(maxLength: 50, unicode: false),
                        Cost = c.Decimal(precision: 18, scale: 0),
                        Servings = c.Byte(),
                        CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.RecipeIngredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecipeID = c.Int(nullable: false),
                        IngredientID = c.Int(nullable: false),
                        Quantity = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ingredients", t => t.IngredientID)
                .ForeignKey("dbo.Recipes", t => t.RecipeID, cascadeDelete: true)
                .Index(t => t.RecipeID)
                .Index(t => t.IngredientID);
            
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Steps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, unicode: false, storeType: "text"),
                        Order = c.Byte(),
                        RecipeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Recipes", t => t.RecipeID, cascadeDelete: true)
                .Index(t => t.RecipeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Steps", "RecipeID", "dbo.Recipes");
            DropForeignKey("dbo.RecipeIngredients", "RecipeID", "dbo.Recipes");
            DropForeignKey("dbo.RecipeIngredients", "IngredientID", "dbo.Ingredients");
            DropForeignKey("dbo.Recipes", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Steps", new[] { "RecipeID" });
            DropIndex("dbo.RecipeIngredients", new[] { "IngredientID" });
            DropIndex("dbo.RecipeIngredients", new[] { "RecipeID" });
            DropIndex("dbo.Recipes", new[] { "CategoryId" });
            DropTable("dbo.Steps");
            DropTable("dbo.Ingredients");
            DropTable("dbo.RecipeIngredients");
            DropTable("dbo.Recipes");
            DropTable("dbo.Categories");
        }
    }
}
