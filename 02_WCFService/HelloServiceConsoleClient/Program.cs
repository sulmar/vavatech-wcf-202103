using Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace HelloServiceConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. TODO: Add Service Reference
            // WSDL http://localhost:5000/HelloService?wsdl

            // 2. Klasa Proxy

            ProxyClassTest();

            ProxyClassCodeTest();

            // 3. Channel Factory

            ChannelFactoryTest();

            ChannelFactoryCodeTest();

        }

        // Konfiguracja z pliku
        private static void ChannelFactoryTest()
        {
            string endpointConfigurationName = "BasicHttpBinding_IHelloService";

            ChannelFactory<IHelloServices.IHelloService> proxy = new ChannelFactory<IHelloServices.IHelloService>(endpointConfigurationName);

            IHelloServices.IHelloService helloService = proxy.CreateChannel();

            helloService.Send("Hello World!");

            string response = helloService.Ping("Hello World!");

            int result = helloService.Add(1, 2);

            Console.WriteLine(result);

            Customer customer = helloService.Get(1);

            Console.WriteLine($"{customer.FirstName} {customer.LastName}");
        }

        // Konfiguracja z kodu
        private static void ChannelFactoryCodeTest()
        {
            Uri uri = new Uri(ConfigurationManager.AppSettings["HelloServiceAddress"]);

            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress endpoint = new EndpointAddress(uri);

            ChannelFactory<IHelloServices.IHelloService> proxy = new ChannelFactory<IHelloServices.IHelloService>(binding, endpoint);

            IHelloServices.IHelloService helloService = proxy.CreateChannel();

            helloService.Send("Hello World!");

            string response = helloService.Ping("Hello World!");

            int result = helloService.Add(1, 2);

            Console.WriteLine(result);

            Customer customer = helloService.Get(1);

            Console.WriteLine($"{customer.FirstName} {customer.LastName}");
        }

        // Konfiguracja z pliku
        private static void ProxyClassTest()
        {
            string endpointConfigurationName = "BasicHttpBinding_IHelloService";

            HelloServiceProxy client = new HelloServiceProxy(endpointConfigurationName);

            client.Send("Hello World!");

            string response = client.Ping("Hello World!");

            Console.WriteLine(response);

            Customer customer = client.Get(1);

            Console.WriteLine($"{customer.FirstName} {customer.LastName}");
        }

        // Konfiguracja z kodu
        private static void ProxyClassCodeTest()
        {
            Uri uri = new Uri(ConfigurationManager.AppSettings["HelloServiceAddress"]);

            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress endpoint = new EndpointAddress(uri);

            HelloServiceProxy client = new HelloServiceProxy(binding, endpoint);

            client.Send("Hello World!");

            string response = client.Ping("Hello World!");

            Console.WriteLine(response);

            Customer customer = client.Get(1);

            Console.WriteLine($"{customer.FirstName} {customer.LastName}");
        }
    }


    // add-reference System.ServiceModel
    public class HelloServiceProxy : ClientBase<IHelloServices.IHelloService>, IHelloServices.IHelloService
    {
        public HelloServiceProxy(string endpointConfigurationName)
            : base(endpointConfigurationName)
        {

        }

        public HelloServiceProxy(Binding binding, EndpointAddress address)
            : base(binding, address)
        {

        }

        public int Add(int x, int y)
        {
            throw new NotImplementedException();
        }

        public decimal Calculate(decimal amount, decimal tax)
        {
            throw new NotSupportedException();
        }

        public Customer Get(int id)
        {
            return this.Channel.Get(id);
        }

        public string Ping(string message)
        {
            return this.Channel.Ping(message);
        }

        public void Send(string content)
        {
            this.Channel.Send(content);
        }
    }
}
