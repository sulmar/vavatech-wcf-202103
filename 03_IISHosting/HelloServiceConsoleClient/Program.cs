using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloServiceConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {

            Vavatech.HelloServiceClient client = new Vavatech.HelloServiceClient();

            string result = client.GetData(10);
        }
    }
}
