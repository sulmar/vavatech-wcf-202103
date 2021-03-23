using Bogus;
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
    public class ProductFaker : Faker<Product>
    {
        public ProductFaker()
        {
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.Name, f => f.Commerce.ProductName());
            RuleFor(p => p.Color, f => f.Commerce.Color());
            RuleFor(p => p.UnitPrice, f => decimal.Parse(f.Commerce.Price()));

        }
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)] // <- singleton
    public class FakeProductService : IProductService
    {
        private readonly ICollection<Product> products;

        public FakeProductService()
        {
            ProductFaker faker = new ProductFaker();

            products = faker.Generate(100);
        }

        public void Add(Product product)
        {
            products.Add(product);

            throw new WebFaultException(System.Net.HttpStatusCode.Created);
        }

        public IEnumerable<Product> Get()
        {
            return products;
        }

        public IEnumerable<Product> GetByCriteria(decimal from, decimal to, string color)
        {
            IQueryable<Product> query = products.AsQueryable();

            query = query.Where(p => p.UnitPrice >= from);

            query = query.Where(p => p.UnitPrice <= to);

            if (!string.IsNullOrEmpty(color))
                query = query.Where(p => p.Color == color);

            return query.ToList();
        }

        public Product GetById(string id)
        {
            Product product = GetById(int.Parse(id));

            if (product == null)
            {
                throw new WebFaultException(System.Net.HttpStatusCode.NotFound);
            }

            return product;
        }

        public Product GetById(int id)
        {
            return products.SingleOrDefault(p => p.Id == id);
        }

        public void Remove(string id)
        {
            Remove(int.Parse(id));
        }

        public void Remove(int id)
        {
            products.Remove(GetById(id));
        }

        public void Update(string id, Product product)
        {
            Update(int.Parse(id), product);
        }

        public void Update(int id, Product product)
        {
            if (id != product.Id)
            {
                // throw new WebFaultException<string>("id is invalid", System.Net.HttpStatusCode.BadRequest);

                throw new WebFaultException<InvalidIdWebFault>(new InvalidIdWebFault { Code = 101, Description = "Id jest niezgodne" }, System.Net.HttpStatusCode.BadRequest);
            }

            Product existingProduct = GetById(id);


            existingProduct.Name = product.Name;
            existingProduct.Color = product.Color;
            existingProduct.UnitPrice = product.UnitPrice;
        }
    }

    public class InvalidIdWebFault
    {
        public int Code { get; set; }
        public string Description { get; set; }
    }
}
