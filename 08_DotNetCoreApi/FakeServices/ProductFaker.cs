using Bogus;
using Models;

namespace FakeServices
{
    public class ProductFaker : Faker<Product>
    {
        public ProductFaker()
        {
            UseSeed(1);
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.Name, f => f.Commerce.ProductName());
            RuleFor(p => p.Color, f => f.Commerce.Color());
            RuleFor(p => p.UnitPrice, f => decimal.Parse(f.Commerce.Price()));
            RuleFor(p => p.BarCode, f => f.Commerce.Ean13());
        }
    }
}
