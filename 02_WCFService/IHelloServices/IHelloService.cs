using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace IHelloServices
{
    // Add reference System.ServiceModel

    [ServiceContract]           // <- atrybut
    public interface IHelloService
    {
        [OperationContract]
        string Ping(string message);

        [OperationContract]
        void Send(string content);

        [OperationContract]
        int Add(int x, int y);

        decimal Calculate(decimal amount, decimal tax);

    }
}
