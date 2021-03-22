using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SerializationConsoleClient.CustomAtrributes;

namespace SerializationConsoleClient.Models
{
    [Display("Klient")]
    public class Customer
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
}
