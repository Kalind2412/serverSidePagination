namespace NimapInfotechDemo.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using NimapInfotechDemo.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<NimapInfotechDemo.DAL.NimapInfotechContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NimapInfotechDemo.DAL.NimapInfotechContext context)
        {
            var categories = new List<Category>
            {
                new Category {CategoryName="Apparel"},
                new Category {CategoryName="FootWear"},
                new Category {CategoryName="Bag"},
                new Category {CategoryName="Accessory"}
            };
            categories.ForEach(s => context.Categories.AddOrUpdate(p => p.CategoryName, s));
            context.SaveChanges();

            var products = new List<Product>
            {
                new Product { 
                    ProductName = "T-Shirt",
                    CategoryName="Apparel",
                    CategoryID = categories.Single(s=>s.CategoryName == "Apparel").CategoryID
                },
                new Product { 
                    ProductName = "Jeans",
                    CategoryName="Apparel",
                    CategoryID = categories.Single(s=>s.CategoryName == "Apparel").CategoryID
                },
                new Product { 
                    ProductName = "Shoes",
                    CategoryName="FootWear",
                    CategoryID = categories.Single(s=>s.CategoryName == "FootWear").CategoryID
                },
                new Product { 
                    ProductName = "Flip-Flops",
                    CategoryName="FootWear",
                    CategoryID = categories.Single(s=>s.CategoryName == "FootWear").CategoryID
                },
                new Product { 
                    ProductName = "HandBag",
                    CategoryName="Bag",
                    CategoryID = categories.Single(s=>s.CategoryName == "Bag").CategoryID
                },
                new Product { 
                    ProductName = "KeyChain",
                    CategoryName="Accessory",
                    CategoryID = categories.Single(s=>s.CategoryName == "Accessory").CategoryID
                }
            };
            foreach (Product p in products)
            {
                var enrollmentInDataBase = context.Products.Where(
                    s => s.Category.CategoryID == p.CategoryID && s.Category.CategoryName == p.CategoryName).SingleOrDefault();
                if (enrollmentInDataBase == null)
                {
                    context.Products.Add(p);
                }
            }
            context.SaveChanges();
        }
    }
}
