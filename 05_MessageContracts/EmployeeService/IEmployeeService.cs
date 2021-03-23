using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeServices
{
    [ServiceContract]
    public interface IEmployeeService
    {
        [OperationContract]
        EmployeeResponse Get(EmployeeRequest request);
    }

    [MessageContract]
    public class EmployeeRequest
    {
        [MessageHeader(Namespace = "http://vavatech.pl/")]
        public string LicenseKey { get; set; }

        [MessageBodyMember]
        public int EmployeeId { get; set; }
    }

    [MessageContract(WrapperNamespace = "http://vavatech.pl/EmployeeResponse")]
    public class EmployeeResponse
    {
        [MessageBodyMember(Order = 1)]
        public int Id { get; set; }
        [MessageBodyMember(Order = 2)] 
        public string FirstName { get; set; }
        [MessageBodyMember(Order = 3)] 
        public string LastName { get; set; }
        [MessageHeader]
        public string Version { get; set; }
    }


}
