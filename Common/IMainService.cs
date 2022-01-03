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
        float GetConsumption(int id, float clientConsumption); //Customer
        [OperationContract]
        bool ChangeSmartMeterID(int id, int newID); //Operator
        [OperationContract]
        bool ChangeClientsConsumption(int id, float newConsumption); //Operator
        [OperationContract]
        bool InstallSmartMeter(int id, string user, float consumption); //Admin
        [OperationContract]
        bool RemoveSmartMeter(int id); //Admin
        [OperationContract]
        bool DeleteDataBase(); //Super-Admin
        [OperationContract]
        bool ArchiveDataBase(); //Super-Admin

    }
}
