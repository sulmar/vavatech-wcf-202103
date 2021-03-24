using IServices;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiService.Controllers
{
    // api/products
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {

        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }


        // GET api/products
        [HttpGet]
        public IActionResult Get()
        {
            var products = productService.Get();

            return Ok(products);
        }

        // GET api/products/{id}
        //[HttpGet]
        //[Route("{id}")]

        [HttpGet("{id:int}", Order = 2, Name = "GetProductById")]
        public IActionResult Get(int id)
        {
            Product product = productService.Get(id);

            return Ok(product);
        }


        [HttpGet]
        public IEnumerable<Product> Get([FromQuery] ProductSearchCriteria searchCriteria)
        {
            var products = productService.Get(searchCriteria);

            return products;
        }

        [HttpGet("{barcode:length(13)}", Order = 1)]
        public IActionResult Get(string barcode)
        {
            Product product = productService.Get(barcode);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Post(Product product)
        {
            productService.Add(product);

            return CreatedAtRoute("GetProductById", new { Id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public void Put(int id, Product product)
        {
            productService.Update(product);
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            productService.Remove(id);
        }
    }
}
