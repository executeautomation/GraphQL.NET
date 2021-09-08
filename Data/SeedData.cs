using System.Collections.Generic;
using System.Linq;

namespace GraphQLProductApp.Data
{
    public static class SeedData
    {
        public static void Seed(this ProductDbContext productDbContext) {

            if (!productDbContext.Products.Any()) {
                 var products = new List<Product>()
                    {
                        new Product()
                        {
                            Name = "Keyboard",
                            Description = "Gaming Keyboard with lights",
                            Price = 150,
                            ProductType = ProductType.PERIPHARALS,
                            Components =
                                new List<Components>()
                                {
                                    new Components()
                                    {
                                        Name = "Keys",
                                        Description = "Glowing Keys"
                                    },
                                    new Components()
                                    {
                                        Name = "Stickers",
                                        Description = "Key stickers"
                                    }
                                }
                        },
                        new Product()
                        {
                            Name = "Mouse",
                            Description = "Gaming Mouse",
                            Price = 40,
                            ProductType = ProductType.PERIPHARALS,
                            Components =
                                new List<Components>()
                                {
                                    new Components()
                                    {
                                        Name = "Wheels",
                                        Description = "Mouse wheels"
                                    },
                                    new Components()
                                    { Name = "LED", Description = "Mouse LEDs" }
                                }
                        },
                        new Product()
                        {
                            Name = "Monitor",
                            Description = "HD monitor",
                            Price = 400,
                            ProductType = ProductType.MONITOR,
                            Components =
                                new List<Components>()
                                {
                                    new Components()
                                    {
                                        Name = "Screen",
                                        Description = "Monitor screen"
                                    },
                                    new Components()
                                    {
                                        Name = "Cables",
                                        Description = "Screen cables"
                                    },
                                    new Components()
                                    { Name = "LED", Description = "Mouse LEDs" }
                                }
                        },
                        new Product()
                        {
                            Name = "Cabinet",
                            ProductType = ProductType.EXTERNAL,
                            Description = "Business Cabinet with lights",
                            Price = 60
                        }
                    };

                productDbContext.Products.AddRange(products);
                productDbContext.SaveChanges();
    
            }
        }
    }
}