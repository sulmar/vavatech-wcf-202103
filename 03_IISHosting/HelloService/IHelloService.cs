using IISHosting.Models;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace HelloService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IHelloService
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        Customer GetCustomer(int id);

        [OperationContract]
        [FaultContract(typeof(EmployeeNotFoundFault))]
        [FaultContract(typeof(SqlFault))]
        Employee GetEmployee(int id);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }

    [DataContract]
    public class EmployeeNotFoundFault
    {
        [DataMember]
        public int Code { get; set; }
        [DataMember] 
        public string Description { get; set; }
    }

    [DataContract]
    public class SqlFault
    {
        public SqlFault()
        {

        }

        public SqlFault(int code, string description)
        {
            Code = code;
            Description = description;
        }

        [DataMember]
        public int Code { get; set; }
        [DataMember]
        public string Description { get; set; }

    }

}
