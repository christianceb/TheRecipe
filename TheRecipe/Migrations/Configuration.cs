namespace TheRecipe.Migrations
{
  using System.Data.Entity.Migrations;

  internal sealed class Configuration : DbMigrationsConfiguration<TheRecipe.RecipesModel>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = false;
    }

    protected override void Seed(TheRecipe.RecipesModel context)
    {
      context.Categories.AddOrUpdate(c => c.Id,
        new Category() { Id = 1, Name = "Dessert" },
        new Category() { Id = 2, Name = "Indian" },
        new Category() { Id = 3, Name = "Pizza" },
        new Category() { Id = 4, Name = "Japanese" },
        new Category() { Id = 5, Name = "Burger" },
        new Category() { Id = 6, Name = "Chinese" },
        new Category() { Id = 7, Name = "Italian" },
        new Category() { Id = 8, Name = "Thai" },
        new Category() { Id = 9, Name = "Asian" },
        new Category() { Id = 10, Name = "Fast Food" }
      );

      context.Ingredients.AddOrUpdate(i => i.Id,
        new Ingredient { Id = 1, Name = "Canola oil" },
        new Ingredient { Id = 2, Name = "Extra-virgin olive oil" },
        new Ingredient { Id = 3, Name = "Toasted sesame" },
        new Ingredient { Id = 4, Name = "Vinegar (balsamic)" },
        new Ingredient { Id = 5, Name = "Vinegar (distilled white)" },
        new Ingredient { Id = 6, Name = "Vinegar (red wine)" },
        new Ingredient { Id = 7, Name = "Vinegar (rice)" },
        new Ingredient { Id = 8, Name = "Ketchup" },
        new Ingredient { Id = 9, Name = "Mayonnaise" },
        new Ingredient { Id = 10, Name = "Dijon mustard" },
        new Ingredient { Id = 11, Name = "Soy sauce" },
        new Ingredient { Id = 12, Name = "Chili paste" },
        new Ingredient { Id = 13, Name = "Hot sauce" },
        new Ingredient { Id = 14, Name = "Worcestershire" },
        new Ingredient { Id = 15, Name = "Kosher salt" },
        new Ingredient { Id = 16, Name = "Black peppercorns" },
        new Ingredient { Id = 17, Name = "Bay leaves (dried)" },
        new Ingredient { Id = 18, Name = "Cayenne pepper (dried)" },
        new Ingredient { Id = 19, Name = "Crushed red pepper (dried)" },
        new Ingredient { Id = 20, Name = "Cumin (dried)" },
        new Ingredient { Id = 21, Name = "Ground coriander (dried)" },
        new Ingredient { Id = 22, Name = "Oregano (dried)" },
        new Ingredient { Id = 23, Name = "Paprika (dried)" },
        new Ingredient { Id = 24, Name = "Rosemary (dried)" },
        new Ingredient { Id = 25, Name = "Thyme leaves (dried)" },
        new Ingredient { Id = 26, Name = "Cinnamon (dried)" },
        new Ingredient { Id = 27, Name = "Cloves (dried)" },
        new Ingredient { Id = 28, Name = "Allspice (dried)" },
        new Ingredient { Id = 29, Name = "Ginger (dried)" },
        new Ingredient { Id = 30, Name = "Nutmeg (dried)" },
        new Ingredient { Id = 31, Name = "Chili powder" },
        new Ingredient { Id = 32, Name = "Curry powder" },
        new Ingredient { Id = 33, Name = "Italian seasoning" },
        new Ingredient { Id = 34, Name = "Vanilla extract" },
        new Ingredient { Id = 35, Name = "Black beans" },
        new Ingredient { Id = 36, Name = "Cannellini beans" },
        new Ingredient { Id = 37, Name = "Chickpeas beans" },
        new Ingredient { Id = 38, Name = "Kidney beans" },
        new Ingredient { Id = 39, Name = "Capers" },
        new Ingredient { Id = 40, Name = "Olives" },
        new Ingredient { Id = 41, Name = "Peanut butter" },
        new Ingredient { Id = 42, Name = "Jelly" },
        new Ingredient { Id = 43, Name = "Stock (low-sodium)" },
        new Ingredient { Id = 44, Name = "Broth (low-sodium)" },
        new Ingredient { Id = 45, Name = "Canned tomatoes" },
        new Ingredient { Id = 46, Name = "Tomato (canned)" },
        new Ingredient { Id = 47, Name = "Tomato paste" },
        new Ingredient { Id = 48, Name = "Salsa" },
        new Ingredient { Id = 49, Name = "Tuna fish" },
        new Ingredient { Id = 50, Name = "Breadcrumbs (regular)" },
        new Ingredient { Id = 51, Name = "Breadcrumbs (panko)" },
        new Ingredient { Id = 52, Name = "Couscous" },
        new Ingredient { Id = 53, Name = "Dried lentils" },
        new Ingredient { Id = 54, Name = "Pasta (regular)" },
        new Ingredient { Id = 55, Name = "Pasta (whole wheat)" },
        new Ingredient { Id = 56, Name = "Rice" },
        new Ingredient { Id = 57, Name = "Rolled oats" },
        new Ingredient { Id = 58, Name = "Barley (dried)" },
        new Ingredient { Id = 59, Name = "Millet (dried)" },
        new Ingredient { Id = 60, Name = "Quinoa (dried)" },
        new Ingredient { Id = 61, Name = "Wheatberries (dried)" },
        new Ingredient { Id = 62, Name = "Baking powder" },
        new Ingredient { Id = 63, Name = "Baking soda" },
        new Ingredient { Id = 64, Name = "Brown sugar" },
        new Ingredient { Id = 65, Name = "Cornstarch" },
        new Ingredient { Id = 66, Name = "All-purpose flour" },
        new Ingredient { Id = 67, Name = "Granulated sugar" },
        new Ingredient { Id = 68, Name = "Honey" },
        new Ingredient { Id = 69, Name = "Butter" },
        new Ingredient { Id = 70, Name = "Sharp cheddar cheese" },
        new Ingredient { Id = 71, Name = "Feta cheese" },
        new Ingredient { Id = 72, Name = "Parmesan cheese" },
        new Ingredient { Id = 73, Name = "Mozzarella cheese" },
        new Ingredient { Id = 74, Name = "Large eggs" },
        new Ingredient { Id = 75, Name = "Milk" },
        new Ingredient { Id = 76, Name = "Plain yogurt" },
        new Ingredient { Id = 77, Name = "Corn tortillas" },
        new Ingredient { Id = 78, Name = "Frozen blackberries" },
        new Ingredient { Id = 79, Name = "Frozen blueberries" },
        new Ingredient { Id = 80, Name = "Frozen peaches" },
        new Ingredient { Id = 81, Name = "Frozen strawberries" },
        new Ingredient { Id = 82, Name = "Frozen broccoli" },
        new Ingredient { Id = 83, Name = "Frozen bell pepper and onion mix" },
        new Ingredient { Id = 84, Name = "Frozen corn" },
        new Ingredient { Id = 85, Name = "Frozen edamame" },
        new Ingredient { Id = 86, Name = "Frozen peas" },
        new Ingredient { Id = 87, Name = "Frozen spinach" },
        new Ingredient { Id = 88, Name = "Garlic" },
        new Ingredient { Id = 89, Name = "Red Onions" },
        new Ingredient { Id = 90, Name = "Yellow Onions" },
        new Ingredient { Id = 91, Name = "Potatoes" },
        new Ingredient { Id = 92, Name = "Dried raisins" },
        new Ingredient { Id = 93, Name = "Dried apples" },
        new Ingredient { Id = 94, Name = "Dried apricots" },
        new Ingredient { Id = 95, Name = "Almonds" },
        new Ingredient { Id = 96, Name = "Peanuts" },
        new Ingredient { Id = 97, Name = "Sunflower seeds" }
      );

      context.Recipes.AddOrUpdate(r => r.Id,
        new Recipe { Id = 1, Title = "Lasagna", Cost = 35, Time = 270, Servings = 10, CategoryId = 7 },
        new Recipe { Id = 2, Title = "Adobo", Cost = 10, Time = 30, Servings = 4, CategoryId = 9 }
      );

      context.Steps.AddOrUpdate(s => s.Id,
        new Step { Id = 1, Order = 0, Content = "Ragu:", RecipeID = 1 },
        new Step { Id = 2, Order = 1, Content = "Heat oil in a large heavy based pot over medium heat. Add garlic, onion, celery and carrots. Cook for 10 minutes until softened and sweet - they should not brown (if they do, turn heat down).", RecipeID = 1 },
        new Step { Id = 3, Order = 2, Content = "Add beef, turn heat up and cook the beef, breaking it up as you go.", RecipeID = 1 },
        new Step { Id = 4, Order = 3, Content = "Once the beef has all turned brown, add the remaining Ragu ingredients EXCEPT the sugar.", RecipeID = 1 },
        new Step { Id = 5, Order = 4, Content = "Stir then adjust the heat so it is bubbling very gently. Place the lid on and cook for 1.5 - 2 hours, stirring every now and then, then remove the lid and simmer for 30 minutes.", RecipeID = 1 },
        new Step { Id = 6, Order = 5, Content = "The ragu is ready when the meat is really tender and the sauce has thickened and is rich - see video for consistency (Note 6). Adjust salt and pepper to taste, and add sugar if required (Note 3)", RecipeID = 1 },
        new Step { Id = 7, Order = 6, Content = "Cheese Sauce:", RecipeID = 1 },
        new Step { Id = 8, Order = 7, Content = "Warm milk up in a saucepan (optional - just makes sauce thicken faster).", RecipeID = 1 },
        new Step { Id = 9, Order = 8, Content = "In a large saucepan, melt butter over medium low heat. Add flour and mix constantly for 1 minute.", RecipeID = 1 },
        new Step { Id = 10, Order = 9, Content = "Pour about 1 cup of the milk in, mixing as you go to incorporate into the flour mixture. Once mostly lump free, add remaining milk. Use a whisk if needed to make it lump free.", RecipeID = 1 },
        new Step { Id = 11, Order = 10, Content = "Turn heat up to medium high. Stir occasionally at first then regularly after a few minutes until sauce thickens - about 5 - 8 minutes. It should coat the back of the wooden spoon.", RecipeID = 1 },
        new Step { Id = 12, Order = 11, Content = "Remove from heat, add cheese, nutmeg, salt and pepper. Mix until the cheese is melted. The Sauce should be thick but still easily pourable - the consistency of heavy cream (you need to be able to drizzle it over the Ragu when layering - see video). If it's too thick, add a splash of water or milk.", RecipeID = 1 },
        new Step { Id = 13, Order = 12, Content = "Assemble:", RecipeID = 1 },
        new Step { Id = 14, Order = 13, Content = "Preheat oven to 180�C/350�F.", RecipeID = 1 },
        new Step { Id = 15, Order = 14, Content = "Use a 33 x 22 x 7 cm / 13 x 9 x 2.5\" baking dish.", RecipeID = 1 },
        new Step { Id = 16, Order = 15, Content = "Smear a bit of Ragu on the base, then cover with lasagna sheets. Tear sheets to fit.", RecipeID = 1 },
        new Step { Id = 17, Order = 16, Content = "Spread over 2 1/2 cups of Ragu (enough to cover sheets), then drizzle over 1 cup of Cheese Sauce.", RecipeID = 1 },
        new Step { Id = 18, Order = 17, Content = "Top with lasagna sheets (Note 7). Spread with another 2 1/2 cups of Ragu, then 1 cup of Cheese Sauce. Top with lasagna sheets then repeat 1 more time.", RecipeID = 1 },
        new Step { Id = 19, Order = 18, Content = "Top with a 4th layer of lasagna sheets, then pour over the remaining Cheese Sauce.", RecipeID = 1 },
        new Step { Id = 20, Order = 19, Content = "Sprinkle with Mozzarella, then bake for 25 minutes or until golden and bubbling.", RecipeID = 1 },
        new Step { Id = 21, Order = 20, Content = "Stand for 5 to 10 minutes before cutting and serving, garnished with basil or parsley if desired.", RecipeID = 1 },
        new Step { Id = 22, Order = 0, Content = "Put all ingredients in bowl", RecipeID = 2 },
        new Step { Id = 23, Order = 1, Content = "Cook the fn rest of it", RecipeID = 2 }
      );

      context.RecipeIngredients.AddOrUpdate(r => r.Id,
        new RecipeIngredient { Id = 1, RecipeID = 1, IngredientID = 2, Quantity = "1 tbsp" },
        new RecipeIngredient { Id = 2, RecipeID = 1, IngredientID = 9, Quantity = "1 whole" },
        new RecipeIngredient { Id = 3, RecipeID = 1, IngredientID = 88, Quantity = "2 cloves" },
        new RecipeIngredient { Id = 4, RecipeID = 1, IngredientID = 17, Quantity = "2" },
        new RecipeIngredient { Id = 5, RecipeID = 1, IngredientID = 25, Quantity = "1/2 tsp" },
        new RecipeIngredient { Id = 6, RecipeID = 1, IngredientID = 22, Quantity = "1/2 tsp" },
        new RecipeIngredient { Id = 7, RecipeID = 1, IngredientID = 14, Quantity = "2 tsp" },
        new RecipeIngredient { Id = 8, RecipeID = 1, IngredientID = 67, Quantity = "1-2 tsp" },
        new RecipeIngredient { Id = 9, RecipeID = 1, IngredientID = 15, Quantity = "1-2 tsp" },
        new RecipeIngredient { Id = 10, RecipeID = 1, IngredientID = 16, Quantity = "1-2 tsp" },
        new RecipeIngredient { Id = 11, RecipeID = 1, IngredientID = 69, Quantity = "60g / 4tbsp" },
        new RecipeIngredient { Id = 12, RecipeID = 1, IngredientID = 66, Quantity = "1/2 (75g)" },
        new RecipeIngredient { Id = 13, RecipeID = 1, IngredientID = 75, Quantity = "4 cups (1 liter)" },
        new RecipeIngredient { Id = 14, RecipeID = 1, IngredientID = 70, Quantity = "2 cups (200g)" },
        new RecipeIngredient { Id = 15, RecipeID = 1, IngredientID = 30, Quantity = "pinch" },
        new RecipeIngredient { Id = 16, RecipeID = 1, IngredientID = 15, Quantity = "to taste" },
        new RecipeIngredient { Id = 17, RecipeID = 1, IngredientID = 16, Quantity = "to taste" },
        new RecipeIngredient { Id = 18, RecipeID = 1, IngredientID = 54, Quantity = "350g (12oz)" },
        new RecipeIngredient { Id = 19, RecipeID = 1, IngredientID = 73, Quantity = "1 1/2 cups (150g) shredded" },
        new RecipeIngredient { Id = 20, RecipeID = 2, IngredientID = 88, Quantity = "whole" },
        new RecipeIngredient { Id = 21, RecipeID = 2, IngredientID = 11, Quantity = "the whole bottle" }
      );

      //  This method will be called after migrating to the latest version.

      //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
      //  to avoid creating duplicate seed data.
    }
  }
}
