using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorServices
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class CalculatorService : ICalculatorService
    {
        public int Add(int x, int y)
        {
            return x + y;
        }

        public int Divide(int x, int y)
        {
            if (y == 0)
            {
                DivideByZeroFault divideByZeroFault = new DivideByZeroFault
                {
                    Code = 101,
                    Error = "Divide by zero",
                    Description = "Nie można dzielić przez 0 w zbiorze liczb całkowitych"
                };

                throw new FaultException<DivideByZeroFault>(divideByZeroFault, "DivideByZero");
            }

            return x / y;
        }
    }

    [DataContract]
    public class DivideByZeroFault
    {
        [DataMember]
        public int Code { get; set; }
        [DataMember] 
        public string Error { get; set; }
        [DataMember] 
        public string Description { get; set; }
    }


    public class EmployeeNotFoundFault
    {

    }
}
