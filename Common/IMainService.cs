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
        string GetConsumption(byte[] idAndConsumption); //Customer

        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        [FaultContract(typeof(OperationException))]
        void ChangeSmartMeterID(byte[] idAndNewId); //Operator

        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        [FaultContract(typeof(OperationException))]
        void ChangeClientsConsumption(byte[] idAndNewConsumption); //Operator

        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        [FaultContract(typeof(OperationException))]
        void InstallSmartMeter(byte[] idUserConsumption); //Admin


        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        [FaultContract(typeof(OperationException))]
        void RemoveSmartMeter(byte[] id); //Admin

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
