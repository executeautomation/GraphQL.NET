using System.Collections.Generic;
using System.Linq;

namespace GraphQLProductApp.Data;

public static class SeedData
{
    public static void Seed(this ProductDbContext productDbContext)
    {
        var products = new List<Product>
        {
            new()
            {
                Name = "Keyboard",
                Description = "Gaming Keyboard with lights",
                Price = 150,
                ProductType = ProductType.PERIPHARALS,
                Components = new List<Components>()
            },
            new()
            {
                Name = "Monitor",
                Description = "HD monitor",
                Price = 400,
                ProductType = ProductType.MONITOR,
                Components = new List<Components>()
            },
            new()
            {
                Name = "Mouse",
                Description = "Gaming Mouse",
                Price = 50,
                ProductType = ProductType.PERIPHARALS,
                Components = new List<Components>()
            },
            new()
            {
                Name = "CPU",
                Description = "Intel Core i7",
                Price = 500,
                ProductType = ProductType.PROCESSOR,
                Components = new List<Components>()
            },
            new()
            {
                Name = "RAM",
                Description = "16GB",
                Price = 100,
                ProductType = ProductType.MEMORY,
                Components = new List<Components>()
            }
        };

        products.ForEach(p => productDbContext.Products.Add(p));
        productDbContext.SaveChanges();

        var components = new List<Components>
        {
            new()
            {
                Name = "Keys",
                Description = "Glowing Keys",
                Product = products.FirstOrDefault(p => p.ProductId == 1)
            },
            new()
            {
                Name = "Stickers",
                Description = "Key stickers",
                Product = products.FirstOrDefault(p => p.ProductId == 1)
            },
            new()
            {
                Name = "Monitor Cover",
                Description = "Monitor Cover",
                Product = products.FirstOrDefault(p => p.ProductId == 2)
            },
            new()
            {
                Name = "Mouse Pad",
                Description = "Mouse Pad high quality",
                Product = products.FirstOrDefault(p => p.ProductId == 3)
            },
            new()
            {
                Name = "Mouse Dust cover",
                Description = "Mouse dust cover high quality",
                Product = products.FirstOrDefault(p => p.ProductId == 3)
            },
            new()
            {
                Name = "Thermal Paste",
                Description = "Thermal",
                Product = products.FirstOrDefault(p => p.ProductId == 4)
            },
            new()
            {
                Name = "Thermal Fan",
                Description = "Thermal Fan",
                Product = products.FirstOrDefault(p => p.ProductId == 4)
            },
            new()
            {
                Name = "RAM Heat Sink",
                Description = "RAM heat sink with fan",
                Product = products.FirstOrDefault(p => p.ProductId == 5)
            }
        };

        components.ForEach(c => productDbContext.Components.Add(c));
        productDbContext.SaveChanges();
    }
}