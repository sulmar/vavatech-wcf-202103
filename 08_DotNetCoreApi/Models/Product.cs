using System;

namespace Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal UnitPrice { get; set; }
        public string BarCode { get; set; }
    }
}
