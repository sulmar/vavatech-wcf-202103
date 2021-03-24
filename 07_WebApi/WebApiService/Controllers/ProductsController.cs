using IServices;
using Models;
using Models.SearchCriterias;
using System.Collections.Generic;
using System.Web.Http;

namespace WebApiService.Controllers
{

    // api/products
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }


        // GET api/products

        //[HttpGet]

        //public IEnumerable<Product> Get()
        //{
        //    var products = productService.Get();

        //    return products;
        //}


        // Route Constraints
        // https://docs.microsoft.com/pl-pl/aspnet/web-api/overview/web-api-routing-and-actions/attribute-routing-in-web-api-2#route-constraints


        // GET api/products/{id}

        [HttpGet]
        //[Route("api/products/{id}")]
        [Route("{id:int}", Order = 2, Name = "GetProductById")]
        public IHttpActionResult Get(int id)
        {
            var product = productService.Get(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // GET api/products?from={from}&to={to}&color={color}

        //[HttpGet]
        //public IEnumerable<Product> Get(decimal from, decimal to, string color)
        //{
        //    var products = productService.Get(from, to, color);

        //    return products;
        //}


        [HttpGet]
        public IEnumerable<Product> Get([FromUri] ProductSearchCriteria searchCriteria)
        {
            var products = productService.Get(searchCriteria);

            return products;
        }


        // GET api/products/{barcode}

        [HttpGet]
        [Route("{barcode:length(13)}", Order = 1)]
        public IHttpActionResult Get(string barcode)
        {
            Product product = productService.Get(barcode);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public IHttpActionResult Post(Product product)
        {
            productService.Add(product);

            // return Created($"http://localhost:5000/api/products/{product.Id}", product);

            return CreatedAtRoute("GetProductById", new { Id = product.Id }, product);
        }

        [HttpPut]
        [Route("{id}")]
        public void Put(int id, Product product)
        {
            productService.Update(product);
        }


        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            productService.Remove(id);
        }

    }
}