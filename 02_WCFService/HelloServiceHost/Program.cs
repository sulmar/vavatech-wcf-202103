using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace HelloServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            // add reference System.ServiceModel
            ConfigurationFileTest();

            //   ProgrammingModelTest();

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

        }

        private static void ConfigurationFileTest()
        {
            using (ServiceHost host = new ServiceHost(typeof(FakeHelloServices.FakeHelloService)))
            {
                host.Open();

                Console.WriteLine("Host started on");

                foreach (var uri in host.BaseAddresses)
                {
                    Console.WriteLine(uri);
                }

            }
        }

        private static void ProgrammingModelTest()
        {
            Uri url = new Uri("http://localhost:5000");


            using (ServiceHost host = new ServiceHost(typeof(FakeHelloServices.FakeHelloService), url))
            {
                Binding binding = new BasicHttpBinding();

                //Add a service endpoint
                host.AddServiceEndpoint(typeof(IHelloServices.IHelloService), binding, string.Empty);

                //Enable metadata exchange
                ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
                host.Description.Behaviors.Add(behavior);

                Binding mexBinding = MetadataExchangeBindings.CreateMexHttpBinding();
                host.AddServiceEndpoint(typeof(IMetadataExchange), mexBinding, "mex");

                host.Open();

                Console.WriteLine("Host started on");

                foreach (var uri in host.BaseAddresses)
                {
                    Console.WriteLine(uri);
                }
            }
        }
    }
}
