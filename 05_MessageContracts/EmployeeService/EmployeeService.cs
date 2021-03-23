using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeServices
{
    public class EmployeeService : IEmployeeService
    {
        public EmployeeResponse Get(EmployeeRequest request)
        {
            EmployeeResponse response = new Faker<EmployeeResponse>()
                .RuleFor(p=>p.Id, f=>request.EmployeeId)
                .RuleFor(p=>p.FirstName, f=>f.Person.FirstName)
                .RuleFor(p => p.LastName, f => f.Person.LastName)
                .RuleFor(p=>p.Version, f=>"v1")
                .Generate();

            return response;
            
        }
    }
}
