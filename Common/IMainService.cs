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
        string GetConsumption(int id, string clientConsumption); //Customer

        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        [FaultContract(typeof(OperationException))]
        void ChangeSmartMeterID(int id, int newID); //Operator

        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        [FaultContract(typeof(OperationException))]
        void ChangeClientsConsumption(int id, string newConsumption); //Operator

        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        [FaultContract(typeof(OperationException))]
        void InstallSmartMeter(int id, string user, string consumption); //Admin


        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        [FaultContract(typeof(OperationException))]
        void RemoveSmartMeter(int id); //Admin

        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        [FaultContract(typeof(OperationException))]
        void DeleteDataBase(); //Super-Admin

        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        [FaultContract(typeof(OperationException))]
        void ArchiveDataBase(); //Super-Admin

    }
}
