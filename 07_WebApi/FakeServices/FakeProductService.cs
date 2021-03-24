using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Bogus;
using Models.SearchCriterias;

namespace FakeServices
{


    public class FakeProductService : IProductService
    {
        private readonly ICollection<Product> products;

        public FakeProductService(Faker<Product> faker)
        {
            products = faker.Generate(100);
        }

        public void Calculate(Product product)
        {

        }

        public void Add(Product product)
        {
            int id = products.Max(p => p.Id);

            product.Id = ++id;

            products.Add(product);
        }

        public IEnumerable<Product> Get()
        {
            return products;
        }

        public Product Get(int id)
        {
            return products.SingleOrDefault(p => p.Id == id);
        }

        public Product Get(string barcode)
        {
            return products.SingleOrDefault(p => p.BarCode == barcode);
        }

        public IEnumerable<Product> Get(decimal from, decimal to, string color)
        {
            IQueryable<Product> query = products.AsQueryable();

            query = query.Where(p => p.UnitPrice >= from);

            query = query.Where(p => p.UnitPrice <= to);

            if (!string.IsNullOrEmpty(color))
                query = query.Where(p => p.Color == color);

            return query.ToList();
        }

        public void Update(Product product)
        {
            Product existingProduct = Get(product.Id);

            existingProduct.Name = product.Name;
            existingProduct.Color = product.Color;
            existingProduct.UnitPrice = product.UnitPrice;
        }
        public void Remove(int id)
        {
            products.Remove(Get(id));
        }

        public IEnumerable<Product> Get(ProductSearchCriteria searchCriteria)
        {
            IQueryable<Product> query = products.AsQueryable();

            query = query.Where(p => p.UnitPrice >= searchCriteria.From);

            query = query.Where(p => p.UnitPrice <= searchCriteria.To);

            if (!string.IsNullOrEmpty(searchCriteria.Color))
                query = query.Where(p => p.Color == searchCriteria.Color);

            return query.ToList();
        }
    }
}
