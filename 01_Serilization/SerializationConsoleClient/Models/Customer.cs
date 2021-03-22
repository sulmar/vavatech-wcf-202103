using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SerializationConsoleClient.CustomAtrributes;

namespace SerializationConsoleClient.Models
{
    [Display("Klient")]
    public class PocoCustomer
    {
        [IsRequried]
        public int Id { get; set; }
        
        [Display("Imię")]
        public string FirstName { get; set; }

        [Display("Nazwisko")]
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsRemoved { get; set; }
    }

    [DataContract(Namespace = "http://vavatech.pl/2021/03/22/Customer")]
    public class Customer
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember(Order = 2, Name = "fn")]
        public string FirstName { get; set; }
        [DataMember(Order = 3, Name = "ln")]
        public string LastName { get; set; }
        [DataMember]
        public decimal Salary { get; set; }
        [DataMember(Order = 1, Name = "dob")]
        public DateTime DateOfBirth { get; set; }

        public bool IsRemoved { get; set; }
    }
}
