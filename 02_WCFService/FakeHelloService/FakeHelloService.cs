using Bogus;
using IHelloServices;
using Models;
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

        public Customer Get(int id)
        {
            Customer customer = new Faker<Customer>()
                .RuleFor(p => p.Id, f => id)
                .RuleFor(p => p.FirstName, f => f.Person.FirstName)
                .RuleFor(p => p.LastName, f => f.Person.LastName)
                .RuleFor(p => p.Salary, f => Math.Round(f.Random.Decimal(1m, 1000m),2))
                .RuleFor(p=>p.DateOfBirth, f=>f.Person.DateOfBirth)
                .RuleFor(p=>p.IsRemoved, f=>f.Random.Bool())
                .Generate();

            return customer;
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
