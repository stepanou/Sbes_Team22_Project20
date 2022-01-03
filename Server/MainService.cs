using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.ServiceModel;
using System.Security.Permissions;
using System.Threading;

namespace Server
{
    public class MainService : IMainService
    {


        [OperationBehavior(Impersonation = ImpersonationOption.Required)]
        public bool ArchiveDataBase()
        {
            if (Thread.CurrentPrincipal.IsInRole("Archive"))
            {
                return false;
            }    
            else
            {
                string name = Thread.CurrentPrincipal.Identity.Name;
                DateTime time = DateTime.Now;
                string message = String.Format("Access is denied. User {0} try to call Delete method (time : {1}). " +
                    "For this method need to be member of group Admin.", name, time.TimeOfDay);
                throw new FaultException<SecurityException>(new SecurityException(message));
            }

        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Modify")]
        public bool ChangeClientsConsumption(int id, string newConsumption)
        {
            throw new NotImplementedException();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Modify")]
        public bool ChangeSmartMeterID(int id, int newID)
        {
            throw new NotImplementedException();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Delete")]
        public bool DeleteDataBase()
        {
            throw new NotImplementedException();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Read")]
        public float GetConsumption(int id, string clientConsumption)
        {
            throw new NotImplementedException();
        }

      //  [PrincipalPermission(SecurityAction.Demand, Role = "Insert")]
        public void InstallSmartMeter(int id, string user, string consumption)
        {

            if (Thread.CurrentPrincipal.IsInRole("Insert"))
            {
                string retVal = string.Empty;

                retVal = DataBase.DataBaseManager.InsertEntity(id.ToString(), user, consumption.ToString());

                if (!retVal.Equals(string.Empty))
                {
                    OperationException opEX = new OperationException(retVal);
                    throw new FaultException<OperationException>(opEX,new FaultReason(retVal));
                }
            }
            else
            {
                string name = Thread.CurrentPrincipal.Identity.Name;
                DateTime time = DateTime.Now;
                string message = String.Format("Access is denied. User {0} try to call InstallSmartMeter method (time : {1}). " +
                    "For this method needs to have a \"Insert\" permission.", name, time.TimeOfDay);


                SecurityException secEx = new SecurityException(message);
                throw new FaultException<SecurityException>(secEx,new FaultReason(secEx.Message));
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Remove")]
        public bool RemoveSmartMeter(int id)
        {
            throw new NotImplementedException();
        }
    }
}
