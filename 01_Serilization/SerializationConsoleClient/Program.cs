using SerializationConsoleClient.CustomAtrributes;
using SerializationConsoleClient.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SerializationConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // DataContractSerializerTest();

              DataContractSerizalizerExtensionsTest();

            // DataContractSerizalizerHelperTest();

            // StreamTest();


            // CustomAttributesTest();


            // UsingTest();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        private static void UsingTest()
        {
            Customer customer = new Customer
            {
                Id = 1,
                FirstName = "John",
                LastName = "Smith",
                Salary = 1000,
                DateOfBirth = DateTime.Parse("1980-01-30"),
                IsRemoved = true
            };

            try
            {
                using (CustomerService customerService = new CustomerService())
                {
                    customerService.Process(customer);

                    Console.WriteLine(customer.FirstName);
                } // ->  customerService.Dispose();
            }
            catch (Exception e)
            {

            }
        }

        private static void CustomAttributesTest()
        {
            Customer customer = new Customer
            {
                Id = 1,
                FirstName = "John",
                LastName = "Smith",
                Salary = 1000,
                DateOfBirth = DateTime.Parse("1980-01-30"),
                IsRemoved = true
            };

            // System.Reflection
            Type type = customer.GetType();

            var attributes = System.Attribute.GetCustomAttributes(type).OfType<DisplayAttribute>();

            foreach (DisplayAttribute attribute in attributes)
            {
                Console.WriteLine(attribute.Text);
            }

            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                var propertyAttribute = property.GetCustomAttribute<DisplayAttribute>();

                if (propertyAttribute != null)
                    Console.WriteLine($"{propertyAttribute.Text}");
            }


            Console.WriteLine($"Imię: {customer.FirstName}");
            Console.WriteLine($"Nazwisko: {customer.LastName}");
        }

        private static void StreamTest()
        {
            using (var streamReader = new StreamReader("TestFile.txt"))
            {
                char[] buffer = new char[10];

                while (!streamReader.EndOfStream)
                {
                    string line = streamReader.ReadLine();
                    Console.WriteLine($"Process {line}");

                    // streamReader.ReadBlock(buffer, 0, buffer.Length);
                    // Console.WriteLine(buffer[0] + buffer[1] + buffer[2]+ buffer[3]+ buffer[4]+ buffer[5]+ buffer[6] + buffer[7]);
                }

                // Read the stream as a string, and write the string to the console.
                // Console.WriteLine(sr.ReadToEnd());

            }

            using (var streamWriter = new StreamWriter("TestFile.txt"))
            {
                streamWriter.WriteLine("Lorem ipsum");
            }
        }


        private static void DataContractSerizalizerExtensionsTest()
        {
            Customer customer = new Customer
            {
                Id = 1,
                FirstName = "John",
                LastName = "Smith",
                Salary = 1000,
                DateOfBirth = DateTime.Parse("1980-01-30"),
                IsRemoved = true
            };

            string xml = customer.Serialize();

            Customer deserializedCustomer = xml.Deserialize<Customer>();

            Product product = new Product
            {
                Id = 1, Color = "Red", Name = "Book of WCF", UnitPrice = 199m
            };

            xml = product.Serialize();

        }

        private static void DataContractSerizalizerHelperTest()
        {
            Customer customer = new Customer
            {
                Id = 1,
                FirstName = "John",
                LastName = "Smith",
                Salary = 1000,
                DateOfBirth = DateTime.Parse("1980-01-30"),
                IsRemoved = true
            };

            string xml = DataContractSerizalizerHelper.Serialize(customer);
            Console.WriteLine(xml);

            Customer deserializedCustomer =  DataContractSerizalizerHelper.Deserialize<Customer>(xml);

            if (object.ReferenceEquals(customer, deserializedCustomer))
            {
                Console.WriteLine("the same");
            }
            else
            {
                Console.WriteLine("another");
            }
        }

        private static void DataContractSerializerTest()
        {
            Customer customer = new Customer
            {
                Id = 1,
                FirstName = "John",
                LastName = "Smith",
                Salary = 1000,
                DateOfBirth = DateTime.Parse("1980-01-30"),
                IsRemoved = true
            };

            MemoryStream stream = new MemoryStream();

            // FileStream stream = new FileStream("customer.xml", FileMode.Create);

            // Add reference System.Runtime.Serialization
            DataContractSerializer serializer = new DataContractSerializer(typeof(Customer));
            serializer.WriteObject(stream, customer);

            // Move to begin
            stream.Seek(0, SeekOrigin.Begin);

            // stream.Position = 0;
            

            StreamReader streamReader = new StreamReader(stream);
            string xml = streamReader.ReadToEnd();

        }
    }


    public static class DataContractSerizalizerExtensions
    {
        public static string Serialize<T>(this T o)
        {
            return DataContractSerizalizerHelper.Serialize<T>(o);
        }

        public static T Deserialize<T>(this string xml)
            where T : new()
        {
            return DataContractSerizalizerHelper.Deserialize<T>(xml);
        }
    }


    public static class DataContractSerizalizerHelper
    {
        public static string Serialize<T>(T o)
        {
            using (MemoryStream stream = new MemoryStream())
            {

                // Add reference System.Runtime.Serialization
                DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                serializer.WriteObject(stream, o);

                // Move to begin
                stream.Seek(0, SeekOrigin.Begin);

                StreamReader streamReader = new StreamReader(stream);
                string xml = streamReader.ReadToEnd();

                return xml;
            }           
        }

        public static T Deserialize<T>(string xml)
            where T : new()         // <- constraint
        {
            T deserialized = new T();

            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                deserialized = (T) serializer.ReadObject(stream);
            }

            return deserialized;
        }
    }



    public class CustomerService : IDisposable
    {
        public void Process(Customer customer)
        {
            File.Create("plik.txt");

            string xml = customer.Serialize();

            File.AppendText(xml);          
        }

        public void Dispose()
        {
            File.Delete("plik.txt");
        }
    }
}

