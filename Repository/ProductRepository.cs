using System.Collections.Generic;
using System.Linq;
using GraphQLProductApp.Data;

namespace GraphQLProductApp.Repository;

public interface IProductRepository
{
    Product GetProductById(int id);

    Product GetProductByName(string name);

    List<Product> GetAllProducts();

    Product AddProduct(Product product);
    Product GetProductByIdAndName(int id, string name);
}

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _context;

    public ProductRepository(ProductDbContext context)
    {
        _context = context;
    }

    public List<Product> GetAllProducts()
    {
        return _context.Products.Select(x => new Product
        {
            ProductId = x.ProductId,
            Name = x.Name,
            Price = x.Price,
            ProductType = x.ProductType,
            Components = x.Components.Select(c => new Components
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList()
        }).ToList();
    }

    public Product GetProductById(int id)
    {
        return _context.Products.Select(x => new Product
        {
            ProductId = x.ProductId,
            Name = x.Name,
            Price = x.Price,
            ProductType = x.ProductType,
            Components = x.Components.Select(c => new Components
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList()
        }).FirstOrDefault(x => x.ProductId == id);
    }

    public Product GetProductByName(string name)
    {
        return _context.Products.Select(x => new Product
        {
            ProductId = x.ProductId,
            Name = x.Name,
            Price = x.Price,
            ProductType = x.ProductType,
            Components = x.Components.Select(c => new Components
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList()
        }).FirstOrDefault(x => x.Name == name);
    }

    public Product AddProduct(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
        return product;
    }

    public Product GetProductByIdAndName(int id, string name)
    {
        return _context.Products.Select(x => new Product
        {
            ProductId = x.ProductId,
            Name = x.Name,
            Price = x.Price,
            ProductType = x.ProductType,
            Components = x.Components.Select(c => new Components
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList()
        }).Single(x => x.ProductId == id && x.Name == name);
    }
}