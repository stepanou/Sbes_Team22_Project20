using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
namespace Common
{
    [ServiceContract (CallbackContract = typeof(ICallBackContract))]
    public interface ILoadBalancer
    {
        [OperationContract(IsOneWay =true)]
        void RegisterWorker(string workerID);

        [OperationContract(IsOneWay = true)]
        void UnregisterWorker(string workerID);
    }
}
