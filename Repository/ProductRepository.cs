using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GraphQLProductApp.Data;

namespace GraphQLProductApp.Repository
{
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
        private readonly ProductDbContext context;

        public ProductRepository(ProductDbContext context)
        {
            this.context = context;
        }

        public List<Product> GetAllProducts()
        {
            return context.Products.ToList();
        }

        public Product GetProductById(int id) =>
            context
                .Products
                .Include(x => x.Components)
                .FirstOrDefault(x => x.Id == id);

        public Product GetProductByName(string name)
        {
            return context
                .Products
                .Include(x => x.Components)
                .FirstOrDefault(x => x.Name == name);
        }

        public Product AddProduct(Product product)
        {
            context.Products.Add (product);
            context.SaveChanges();
            return product;
        }

        public Product GetProductByIdAndName(int id, string name)
        {
            return context
                .Products
                .Include(x => x.Components)
                .Single(x => x.Id == id && x.Name == name);
        }
    }
}
