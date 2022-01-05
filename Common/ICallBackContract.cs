using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Common
{
    //[ServiceContract] - moze, a i ne mora
    public interface ICallBackContract
    {
        [OperationContract]
        string DoWork(string consumption);

    }
}
