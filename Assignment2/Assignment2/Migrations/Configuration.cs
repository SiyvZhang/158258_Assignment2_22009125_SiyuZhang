namespace Assignment2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Collections.Generic;
    using System.Linq;
    using Assignment2.Models;
    internal sealed class Configuration : DbMigrationsConfiguration<Assignment2.Data.Assignment2Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Assignment2.Data.Assignment2Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            var categories = new List<Category> {
            new Category { Name="Appetizer"},
            new Category { Name = "Soup" },
            new Category { Name = "Fish" },
            new Category { Name = "Meat" },
            new Category { Name = "Childern's Meal" },
            new Category { Name = "Salad" },
            new Category { Name = "Dessert" },
            new Category { Name = "Drink" },
            };
            categories.ForEach(c => context.Categories.AddOrUpdate(f => f.Name, c));
            context.SaveChanges();
            var foods = new List<Food>
            {
                new Food{Name="Caviar",Description="salted sturgeon roe",Price=100,CategoryID=categories.Single(c=>c.Name=="Appetizer").ID},
                new Food{Name="Goose Liver Paste",Description="Usually used to spread on bread or biscuits",Price=80,CategoryID=categories.Single(c=>c.Name=="Appetizer").ID},
                new Food{Name="Minestrone Soup",Description="An Italian soup with various vegetables and pasta",Price=30,CategoryID=categories.Single(c=>c.Name=="Soup").ID},
                new Food{Name="Borscht",Description="Beet as the main feed",Price=30,CategoryID=categories.Single(c=>c.Name=="Soup").ID},
                new Food{Name="Steamed Fish",Description="Cook with sea bass",Price=50,CategoryID=categories.Single(c=>c.Name=="Fish").ID},
                new Food{Name="Braised Fish in Brown Sauce",Description="Made with pomfret",Price=50,CategoryID=categories.Single(c=>c.Name=="Fish").ID},
                new Food{Name="Black Pepper Beef Cube",Description="Sweet and salty",Price=55,CategoryID=categories.Single(c=>c.Name=="Meat").ID},
                new Food{Name="Beef Wellington",Description="Mix beef, etc., in a meat pie for roasting",Price=65,CategoryID=categories.Single(c=>c.Name=="Meat").ID},
                new Food{Name="Three Color Steamed Egg",Description="Made with eggs, salted duck eggs and preserved eggs",Price=45,CategoryID=categories.Single(c=>c.Name=="Childern's Meal").ID},
                new Food{Name="Steamed Egg with Tofu",Description="The texture is smooth and not greasy",Price=40,CategoryID=categories.Single(c=>c.Name=="Childern's Meal").ID},
                new Food{Name="Green salad",Description="All kinds of vegetables",Price=15,CategoryID=categories.Single(c=>c.Name=="Salad").ID},
                new Food{Name="Main salad",Description="Rich in all kinds of meat, vegetables, pasta and fruits",Price=40,CategoryID=categories.Single(c=>c.Name=="Salad").ID},
                new Food{Name="Tiramisu",Description="It's a cake that smells like coffee wine",Price=20,CategoryID=categories.Single(c=>c.Name=="Dessert").ID},
                new Food{Name="Egg Tart",Description="It's soft and crisp, creamy and eggy",Price=20,CategoryID=categories.Single(c=>c.Name=="Dessert").ID},
                new Food{Name="Coffee",Description="Thick but not bitter, thick glycol",Price=10,CategoryID=categories.Single(c=>c.Name=="Drink").ID},
                new Food{Name="Milk",Description="The milk is fragrant, sweet and delicious",Price=10,CategoryID=categories.Single(c=>c.Name=="Drink").ID},
            };
            foods.ForEach(c => context.Foods.AddOrUpdate(f => f.Name, c));
            context.SaveChanges();
        }
    }
}
