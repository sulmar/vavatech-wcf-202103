using DbServices;
using FakeServices;
using IServices;
using Models;
using Models.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApiService.Controllers
{

    // api/products
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private readonly IProductService productService;

        public ProductsController()
            : this(new FakeProductService(new ProductFaker()))
        {

        }

        //public ProductsController()
        //  : this(new DbProductService(new System.Data.SqlClient.SqlConnection("MyConnectionString")))
        //{

        //}

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
        [Route("{id:int}", Order = 2)]
        public Product Get(int id)
        {
            var product = productService.Get(id);

            return product;
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
        [Route("{barcode:length(10)}", Order = 1)]
        public Product Get(string barcode)
        {
            Product product = productService.Get(barcode);

            return product;
        }

        [HttpPost]
        public void Post(Product product)
        {
            productService.Add(product);
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