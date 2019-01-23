namespace Inlämningsuppgift_MVC.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Inlämningsuppgift_MVC.Models.CategoryDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Inlämningsuppgift_MVC.Models.CategoryDb context)
        {
            /*context.Categories.AddOrUpdate(c => c.CategoryId,
            new Models.Category { CategoryId = 1, Name = "Chocolate" },
            new Models.Category { CategoryId = 2, Name = "Fruits" },
            new Models.Category { CategoryId = 3, Name = "Cheese" },
            new Models.Category { CategoryId = 4, Name = "Beverage" },
            new Models.Category { CategoryId = 5, Name = "Meat" }

            );
            context.SaveChanges();

            context.Products.AddOrUpdate(p => p.ProductId,
                new Models.Product { ProductId = 1, Category = context.Categories.FirstOrDefault(c => c.CategoryId == 1), Name = "Milk Chocolate", Description = "40% kakao", Price = 13 },
                new Models.Product { ProductId = 2, Category = context.Categories.FirstOrDefault(c => c.CategoryId == 1), Name = "Dark Chocolate", Description = "70% kakao", Price = 15 },
                new Models.Product { ProductId = 3, Category = context.Categories.FirstOrDefault(c => c.CategoryId == 2), Name = "Apple", Description = "Green", Price = 4 },
                new Models.Product { ProductId = 4, Category = context.Categories.FirstOrDefault(c => c.CategoryId == 3), Name = "Cheddar", Description = "Milk", Price = 20 },
                new Models.Product { ProductId = 5, Category = context.Categories.FirstOrDefault(c => c.CategoryId == 4), Name = "Coca-Cola", Description = "33cl", Price = 10 },
                new Models.Product { ProductId = 6, Category = context.Categories.FirstOrDefault(c => c.CategoryId == 5), Name = "Beef", Description = "Mooo", Price = 109 }
                );

            context.SaveChanges();*/
        }
    }
}
