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
                .Where(x => x.Id == id)
                .FirstOrDefault();

        public Product GetProductByName(string name)
        {
            return context
                .Products
                .Include(x => x.Components)
                .Where(x => x.Name == name)
                .FirstOrDefault();
        }

        public Product AddProduct(Product product)
        {
            context.Products.Add (product);
            context.SaveChanges();
            return product;
        }
    }
}
