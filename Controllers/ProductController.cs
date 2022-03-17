using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using GraphQLProductApp.Controllers;
using GraphQLProductApp.Data;
using GraphQLProductApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraphQLProductApp.Controllersœ
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ProductController : BaseController
    {
        private readonly IProductRepository _productRepository;
        const String folderName = "files";  
        readonly String folderPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);  


        public ProductController(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
            if (!Directory.Exists(folderPath))  
            {  
                Directory.CreateDirectory(folderPath);  
            }  
        }

        [HttpGet]
        [Route("/[controller]/[action]/{id}")]
        public Product GetProductById(int id)
        {
            return _productRepository.GetProductById(id);
        }
        
        
        [HttpGet]
        [Route("/[controller]/[action]")]
        public Product GetProductByIdAndName([FromQuery]QueryParameter queryParameter)
        {
            return _productRepository.GetProductByIdAndName(queryParameter.Id, queryParameter.Name);
        }

        [HttpGet]
        [Route("/[controller]/[action]/{name}")]
        public Product GetProductByName(string name)
        {
            return _productRepository.GetProductByName(name);
        }

        // GET: ProductController/GetProducts
        [HttpGet]
        [Route("/[controller]/[action]")]
        public ActionResult<List<Product>> GetProducts()
        {
            return _productRepository.GetAllProducts();
        }

        // POST: ProductController/Create
        [HttpPost]
        [Route("/[controller]/[action]")]
        public Product Create(Product product)
        {
            return _productRepository.AddProduct(product);
        }
        

        [HttpPost]  
        public async Task<IActionResult> Post(  
            [FromForm(Name = "myFile")]IFormFile myFile)  
        {  
            using (var fileContentStream = new MemoryStream())  
            {  
                await myFile.CopyToAsync(fileContentStream);  
                await System.IO.File.WriteAllBytesAsync(Path.Combine(folderPath, myFile.FileName), fileContentStream.ToArray());  
            }  
            return CreatedAtRoute(routeName: "myFile", routeValues: new { filename = myFile.FileName }, value: null); ;  
        }  
  
        [HttpGet("{filename}", Name = "myFile")]  
        public async Task<IActionResult> Get([FromRoute] String filename)  
        {  
            var filePath = Path.Combine(folderPath, filename);  
            if (System.IO.File.Exists(filePath))  
            {  
                return File(await System.IO.File.ReadAllBytesAsync(filePath), "application/octet-stream", filename);  
            }  
            return NotFound();  
        }  
    }
}
