using IHelloServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeHelloServices
{
    public class FakeHelloService : IHelloService
    {
        public int Add(int x, int y)
        {
            return x + y;
        }

        public decimal Calculate(decimal amount, decimal tax)
        {
            return amount * tax;
        }

        public string Ping(string message)
        {
            Console.WriteLine($"Ping {message}");

            return $"Pong {message}";
        }

        public void Send(string content)
        {
            Console.WriteLine($"Send {content}");

            Trace.WriteLine($"Send {content}");
        }
    }
}
