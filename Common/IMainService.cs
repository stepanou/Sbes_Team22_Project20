using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Common
{
    [ServiceContract]
    public interface IMainService
    {
        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        [FaultContract(typeof(OperationException))]
        float GetConsumption(int id, string clientConsumption); //Customer

        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        [FaultContract(typeof(OperationException))]
        bool ChangeSmartMeterID(int id, int newID); //Operator

        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        [FaultContract(typeof(OperationException))]
        bool ChangeClientsConsumption(int id, string newConsumption); //Operator

        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        [FaultContract(typeof(OperationException))]
        void InstallSmartMeter(int id, string user, string consumption); //Admin


        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        [FaultContract(typeof(OperationException))]
        bool RemoveSmartMeter(int id); //Admin

        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        [FaultContract(typeof(OperationException))]
        bool DeleteDataBase(); //Super-Admin

        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        [FaultContract(typeof(OperationException))]
        bool ArchiveDataBase(); //Super-Admin

    }
}
