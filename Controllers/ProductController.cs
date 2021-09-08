using System.Collections.Generic;
using GraphQLProductApp.Data;
using GraphQLProductApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GraphQLProductApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        [Route("/[controller]/[action]/{id}")]
        public Product GetProductById(int id)
        {
            return productRepository.GetProductById(id);
        }

        [HttpGet]
        [Route("/[controller]/[action]/{name}")]
        public Product GetProductByName(string name)
        {
            return productRepository.GetProductByName(name);
        }

        // GET: ProductController/GetProducts
        [HttpGet]
        [Route("/[controller]/[action]")]
        public ActionResult<List<Product>> GetProducts()
        {
            return productRepository.GetAllProducts();
        }

        // POST: ProductController/Create
        [HttpPost]
        [Route("/[controller]/[action]")]
        public Product Create(Product product)
        {
            return productRepository.AddProduct(product);
        }
    }
}
