using Models;
using Models.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IProductService
    {
        void Add(Product product);
        IEnumerable<Product> Get();
        Product Get(int id);
        Product Get(string barcode);
        IEnumerable<Product> Get(decimal from, decimal to, string color);
        IEnumerable<Product> Get(ProductSearchCriteria searchCriteria);
        void Update(Product product);
        void Remove(int id);
    }
}
