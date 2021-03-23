using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [KnownType(typeof(FullTimeEmployee))]
    [KnownType(typeof(PartTimeEmployee))]
    [DataContract(Namespace = "http://vavatech.pl/2021/03/23/Employee")]
    public class Employee
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember] 
        public string FirstName { get; set; }
        [DataMember] 
        public string LastName { get; set; }
    }

    [DataContract(Namespace = "http://vavatech.pl/2021/03/23/FullTimeEmployee")]
    public class FullTimeEmployee : Employee
    {
        [DataMember]
        public decimal AnnualSalary { get; set; }

        [DataMember]
        public string Departament { get; set; }
    }

    [DataContract(Namespace = "http://vavatech.pl/2021/03/23/PartTimeEmployee")]
    public class PartTimeEmployee : Employee
    {
        [DataMember]
        public decimal HourlyPay { get; set; }
    }



}
