using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.ServiceModel;
using System.Security.Permissions;
using System.Threading;
using System.Security.Principal;

namespace Server
{
    public class MainService : IMainService
    {


     //   [OperationBehavior(Impersonation = ImpersonationOption.Required)]
        public void ArchiveDataBase()
        {
            if (Thread.CurrentPrincipal.IsInRole("Archive"))
            {
                string retVal = string.Empty;

                Console.WriteLine($"Process Identity:{WindowsIdentity.GetCurrent().Name}");

                retVal = DataBase.DataBaseManager.ArchiveDB();

                if (!retVal.Equals(string.Empty))
                {
                    OperationException opEX = new OperationException(retVal);
                    throw new FaultException<OperationException>(opEX, new FaultReason(retVal));
                }

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

        
        public void ChangeClientsConsumption(int id, string newConsumption)
        {
            if (Thread.CurrentPrincipal.IsInRole("Modify"))
            {
                string retVal = string.Empty;

                retVal = DataBase.DataBaseManager.ModifiyEntity(id.ToString(), newConsumption);

                if (!retVal.Equals(string.Empty))
                {
                    OperationException opEX = new OperationException(retVal);
                    throw new FaultException<OperationException>(opEX, new FaultReason(retVal));
                }
            }
            else
            {
                string name = Thread.CurrentPrincipal.Identity.Name;
                DateTime time = DateTime.Now;
                string message = String.Format("Access is denied. User {0} try to call ChangeClientsConsumption() [time : {1}].\n" +
                    "For this method needs to have a \"MODIFY\" permission.", name, time.TimeOfDay);


                SecurityException secEx = new SecurityException(message);
                throw new FaultException<SecurityException>(secEx, new FaultReason(secEx.Message));
            }
        }

        
        public void ChangeSmartMeterID(int id, int newID)
        {
            if (Thread.CurrentPrincipal.IsInRole("Modify"))
            {
                string retVal = string.Empty;

                retVal = DataBase.DataBaseManager.ModifiyEntityId(id.ToString(), newID.ToString());

                if (!retVal.Equals(string.Empty))
                {
                    OperationException opEX = new OperationException(retVal);
                    throw new FaultException<OperationException>(opEX, new FaultReason(retVal));
                }
            }
            else
            {
                string name = Thread.CurrentPrincipal.Identity.Name;
                DateTime time = DateTime.Now;
                string message = String.Format("Access is denied. User {0} try to call ChangeSmartMeterID() [time : {1}].\n" +
                    "For this method needs to have a \"MODIFY\" permission.", name, time.TimeOfDay);


                SecurityException secEx = new SecurityException(message);
                throw new FaultException<SecurityException>(secEx, new FaultReason(secEx.Message));
            }
        }

      
        public void DeleteDataBase()
        {
            if (Thread.CurrentPrincipal.IsInRole("Delete"))
            {
                string retVal = string.Empty;

                retVal = DataBase.DataBaseManager.DeleteDB();

                if (!retVal.Equals(string.Empty))
                {
                    OperationException opEX = new OperationException(retVal);
                    throw new FaultException<OperationException>(opEX, new FaultReason(retVal));
                }
            }
            else
            {
                string name = Thread.CurrentPrincipal.Identity.Name;
                DateTime time = DateTime.Now;
                string message = String.Format("Access is denied. User {0} try to call DeleteDataBase() [time : {1}].\n" +
                    "For this method needs to have a \"DELETE\" permission.", name, time.TimeOfDay);


                SecurityException secEx = new SecurityException(message);
                throw new FaultException<SecurityException>(secEx, new FaultReason(secEx.Message));
            }
        }

        
        public float GetConsumption(int id, string clientConsumption)
        {
            throw new NotImplementedException();
        }

      
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
                string message = String.Format("Access is denied. User {0} try to call InstallSmartMeter method [time : {1}].\n" +
                    "For this method needs to have a \"INSERT\" permission.", name, time.TimeOfDay);


                SecurityException secEx = new SecurityException(message);
                throw new FaultException<SecurityException>(secEx,new FaultReason(secEx.Message));
            }
        }

      //  [PrincipalPermission(SecurityAction.Demand, Role = "Remove")]
        public void RemoveSmartMeter(int id)
        {
            if (Thread.CurrentPrincipal.IsInRole("Remove"))
            {
                string retVal = string.Empty;

                retVal = DataBase.DataBaseManager.DeleteEntity(id.ToString());

                if (!retVal.Equals(string.Empty))
                {
                    OperationException opEX = new OperationException(retVal);
                    throw new FaultException<OperationException>(opEX, new FaultReason(retVal));
                }
            }
            else
            {
                string name = Thread.CurrentPrincipal.Identity.Name;
                DateTime time = DateTime.Now;
                string message = String.Format("Access is denied. User {0} try to call RemoveSmartMeter() method [time : {1}].\n" +
                    "For this method needs to have a \"REMOVE\" permission.", name, time.TimeOfDay);


                SecurityException secEx = new SecurityException(message);
                throw new FaultException<SecurityException>(secEx, new FaultReason(secEx.Message));
            }
        }


        

    }
}
