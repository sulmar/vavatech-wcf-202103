using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ProductServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            // string baseAddress = ConfigurationManager.AppSettings["baseAddress"];

            using (ServiceHost host = new ServiceHost(typeof(ProductServices.FakeProductService)))
            {
                host.Open();
                Console.WriteLine("Host started on");

                foreach (var uri in host.BaseAddresses)
                {
                    Console.WriteLine(uri);
                }

                Console.WriteLine("Press Enter to exit.");
                Console.ReadLine();
            }

        }
    }
}
