using ProductServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace ProductServices
{
    [ServiceContract]
    public interface IProductService
    {
        // GET api/products (endpoint)
        [OperationContract]
        [WebGet(UriTemplate = "api/products")]
        IEnumerable<Product> Get();

        // GET api/products/{id}
        [OperationContract]
        [WebGet(UriTemplate = "api/products/{id}")]
        Product GetById(string id);

        // GET api/products?from={from}&to={to}&color={color}
        [OperationContract]
        [WebGet(UriTemplate = "api/products?from={from}&to={to}&color={color}")]
        IEnumerable<Product> GetByCriteria(decimal from, decimal to, string color);

        // POST api/products
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "api/products")]
        void Add(Product product);

        // PUT api/products/{id}
        // PATCH api/products/{id}
        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "api/products/{id}")]
        void Update(string id, Product product);

        // DELETE api/products/{id}
        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "api/products/{id}")]
        void Remove(string id);
    }
}
