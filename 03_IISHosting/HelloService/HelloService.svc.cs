using Bogus;
using IISHosting.Models;
using Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace HelloService
{

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class FakeHelloService : IHelloService
    {
        public Customer GetCustomer(int id)
        {
            Customer customer = new CustomerFaker()
               .Generate();

            Customer partner = new CustomerFaker()
               .Generate();

            partner.Partner = customer;

            customer.Partner = partner;

            return customer;
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public Employee GetEmployee(int id)
        {
            if (id == 0)
            {
                EmployeeNotFoundFault fault = new EmployeeNotFoundFault
                {
                    Code = 404,
                    Description = $"Employee id {id} not found"
                };

                throw new FaultException<EmployeeNotFoundFault>(fault, "NotFound");
            }

            FullTimeEmployee customer = new Faker<FullTimeEmployee>()
                .RuleFor(p => p.Id, f => f.IndexFaker)
                .RuleFor(p => p.AnnualSalary, f => f.Random.Decimal(1, 1000))
                .RuleFor(p => p.Departament, f => f.Commerce.Department())
                .RuleFor(p => p.FirstName, f => f.Person.FirstName)
                .RuleFor(p => p.LastName, f => f.Person.LastName)
                .Generate();

            try
            {
                // throw new SqlException("primary key duplicate", null, new Exception(), Guid.NewGuid() );
            }
            catch(SqlException ex)
            {
                throw new FaultException<SqlFault>(new SqlFault(ex.Number, ex.Message));                
            }

            return customer;
        }
    }
}
