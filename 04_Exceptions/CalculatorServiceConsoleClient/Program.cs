using CalculatorServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorServiceConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri uri = new Uri(ConfigurationManager.AppSettings["CalculatorServiceAddress"]);

            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress endpoint = new EndpointAddress(uri);

            ChannelFactory<ICalculatorService> proxy = new ChannelFactory<ICalculatorService>(binding, endpoint);

            ICalculatorService calculatorService = proxy.CreateChannel();

            int result = calculatorService.Add(1, 2);

            Console.WriteLine(result);

            try
            {
                result = calculatorService.Divide(4, 0);
            }
            catch (FaultException<DivideByZeroFault> e)
            {
                Console.WriteLine($"{e.Detail.Code} {e.Detail.Error} {e.Detail.Description}");
            }

            Console.WriteLine("Press key to exit.");
            Console.ReadKey();
        }
    }
}
