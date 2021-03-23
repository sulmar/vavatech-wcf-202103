using IServices;
using Models;
using Models.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbServices
{
    public class DbProductService : IProductService
    {
        private readonly SqlConnection connection;

        public DbProductService(SqlConnection connection)
        {
            this.connection = connection;
        }

        public void Add(Product product)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> Get()
        {
            string sql = "select id, name, color, barcode, unitPrice from dbo.Products";

            ICollection<Product> products = new List<Product>();

            SqlCommand command = new SqlCommand(sql, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                products.Add(Map(reader));
            }

            connection.Close();

            return products;
        }

        private static Product Map(SqlDataReader reader)
        {
            Product product = new Product();
            product.Id = reader.GetInt32(0);
            product.Name = reader.GetString(1);
            product.Color = reader.GetString(2);
            product.BarCode = reader.GetString(3);
            product.UnitPrice = reader.GetDecimal(4);
            return product;
        }

        public Product Get(int id)
        {
            Product product = null;

            string sql = "select id, name, color, barcode, unitPrice from dbo.Products where Id = @Id";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
               product  = Map(reader);

            }

            connection.Close();

            return product;

        }

        public Product Get(string barcode)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> Get(decimal from, decimal to, string color)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> Get(ProductSearchCriteria searchCriteria)
        {
            throw new NotImplementedException();
        }
    }
}
