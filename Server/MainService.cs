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
using SecurityManager;
using System.Globalization;

namespace Server
{
    public class MainService : IMainService
    {


     //   [OperationBehavior(Impersonation = ImpersonationOption.Required)]
        public void ArchiveDataBase()
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            string userName = Formatter.ParseName(principal.Identity.Name);

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

                try
                {
                    Audit.AuthorizationSuccess(userName,
                        OperationContext.Current.IncomingMessageHeaders.Action);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }    
            else
            {
                try
                {
                    Audit.AuthorizationFailed(userName,
                        OperationContext.Current.IncomingMessageHeaders.Action, "ArchiveDataBase method need Archive permission.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                DateTime time = DateTime.Now;
                string message = String.Format("Access is denied. User {0} try to call ArchiveDataBase method (time : {1}). " +
                    "For this method need to be member of group Admin.", userName, time.TimeOfDay);

                SecurityException secEx = new SecurityException(message);
                throw new FaultException<SecurityException>(secEx, new FaultReason(secEx.Message));

            }

        }

        
        public void ChangeClientsConsumption(int id, string newConsumption)
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            string userName = Formatter.ParseName(principal.Identity.Name);


            if (Thread.CurrentPrincipal.IsInRole("Modify"))
            {
                string retVal = string.Empty;

                retVal = DataBase.DataBaseManager.ModifiyEntity(id.ToString(), newConsumption);

                if (!retVal.Equals(string.Empty))
                {
                    OperationException opEX = new OperationException(retVal);
                    throw new FaultException<OperationException>(opEX, new FaultReason(retVal));
                }

                try
                {
                    Audit.AuthorizationSuccess(userName,
                        OperationContext.Current.IncomingMessageHeaders.Action);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                try
                {
                    Audit.AuthorizationFailed(userName,
                        OperationContext.Current.IncomingMessageHeaders.Action, "ChangeClientsConsumption method need Modify permission.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                
                DateTime time = DateTime.Now;
                string message = String.Format("Access is denied. User {0} try to call ChangeClientsConsumption [time : {1}].\n" +
                    "For this method needs to have a \"Modify\" permission.", userName, time.TimeOfDay);


                SecurityException secEx = new SecurityException(message);
                throw new FaultException<SecurityException>(secEx, new FaultReason(secEx.Message));

            }
        }

        
        public void ChangeSmartMeterID(int id, int newID)
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            string userName = Formatter.ParseName(principal.Identity.Name);


            if (Thread.CurrentPrincipal.IsInRole("Modify"))
            {
                string retVal = string.Empty;

                retVal = DataBase.DataBaseManager.ModifiyEntityId(id.ToString(), newID.ToString());

                if (!retVal.Equals(string.Empty))
                {
                    OperationException opEX = new OperationException(retVal);
                    throw new FaultException<OperationException>(opEX, new FaultReason(retVal));
                }

                try
                {
                    Audit.AuthorizationSuccess(userName,
                        OperationContext.Current.IncomingMessageHeaders.Action);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                try
                {
                    Audit.AuthorizationFailed(userName,
                        OperationContext.Current.IncomingMessageHeaders.Action, "ChangeSmartMeterID method need Modify permission.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                
                DateTime time = DateTime.Now;
                string message = String.Format("Access is denied. User {0} try to call ChangeSmartMeterID [time : {1}].\n" +
                    "For this method needs to have a \"Modify\" permission.", userName, time.TimeOfDay);


                SecurityException secEx = new SecurityException(message);
                throw new FaultException<SecurityException>(secEx, new FaultReason(secEx.Message));

            }
        }

      
        public void DeleteDataBase()
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            string userName = Formatter.ParseName(principal.Identity.Name);


            if (Thread.CurrentPrincipal.IsInRole("Delete"))
            {
                string retVal = string.Empty;

                retVal = DataBase.DataBaseManager.DeleteDB();

                if (!retVal.Equals(string.Empty))
                {
                    OperationException opEX = new OperationException(retVal);
                    throw new FaultException<OperationException>(opEX, new FaultReason(retVal));
                }

                try
                {
                    Audit.AuthorizationSuccess(userName,
                        OperationContext.Current.IncomingMessageHeaders.Action);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                try
                {
                    Audit.AuthorizationFailed(userName,
                        OperationContext.Current.IncomingMessageHeaders.Action, "DeleteDataBase method need Delete permission.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                DateTime time = DateTime.Now;
                string message = String.Format("Access is denied. User {0} try to call DeleteDataBase [time : {1}].\n" +
                    "For this method needs to have a \"Delete\" permission.", userName, time.TimeOfDay);


                SecurityException secEx = new SecurityException(message);
                throw new FaultException<SecurityException>(secEx, new FaultReason(secEx.Message));

            }
        }

        
        public string GetConsumption(int id, string clientConsumption)
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            string userName = Formatter.ParseName(principal.Identity.Name);


            if (Thread.CurrentPrincipal.IsInRole("Read"))
            {
                string retVal = string.Empty;
                SmartMeter smartMeter = null;

                smartMeter = DataBase.DataBaseManager.GetEntity(id.ToString());

                if (smartMeter == null)
                {
                    OperationException opEX = new OperationException("SmartMeter does not exist");
                    throw new FaultException<OperationException>(opEX, new FaultReason("SmartMeter does not exist"));
                }

                smartMeter.Consumption += float.Parse(clientConsumption,CultureInfo.InvariantCulture.NumberFormat);

                retVal = DataBase.DataBaseManager.ModifiyEntity(smartMeter.Id.ToString(),smartMeter.Consumption.ToString("0.000"));

                if (!retVal.Equals(string.Empty))
                {
                    OperationException opEX = new OperationException(retVal);
                    throw new FaultException<OperationException>(opEX, new FaultReason(retVal));
                }
                
                    //try
                    //{
                    //    Audit.AuthorizationSuccess(userName,
                    //        OperationContext.Current.IncomingMessageHeaders.Action);
                    //}
                    //catch (Exception e)
                    //{
                    //    Console.WriteLine(e.Message);
                    //}

                string address = "net.tcp://localhost:11012/LoadBalancer";
                ChannelFactory<ICalculatePrice> channelFactory = new ChannelFactory<ICalculatePrice>(new NetTcpBinding(), new EndpointAddress(new Uri(address)));
                ICalculatePrice proxy = channelFactory.CreateChannel();

                retVal = proxy.CalculatePrice(clientConsumption);
                return retVal;

            }
            else
            {
                try
                {
                    Audit.AuthorizationFailed(userName,
                        OperationContext.Current.IncomingMessageHeaders.Action, "ChangeClientsConsumption method need Modify permission.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                DateTime time = DateTime.Now;
                string message = String.Format("Access is denied. User {0} try to call ChangeClientsConsumption [time : {1}].\n" +
                    "For this method needs to have a \"Modify\" permission.", userName, time.TimeOfDay);


                SecurityException secEx = new SecurityException(message);
                throw new FaultException<SecurityException>(secEx, new FaultReason(secEx.Message));

            }


        }

      
        public void InstallSmartMeter(int id, string user, string consumption)
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            string userName = Formatter.ParseName(principal.Identity.Name);


            if (Thread.CurrentPrincipal.IsInRole("Insert"))
            {
                string retVal = string.Empty;

                retVal = DataBase.DataBaseManager.InsertEntity(id.ToString(), user, consumption.ToString());

                if (!retVal.Equals(string.Empty))
                {
                    OperationException opEX = new OperationException(retVal);
                    throw new FaultException<OperationException>(opEX,new FaultReason(retVal));
                }

                try
                {
                    Audit.AuthorizationSuccess(userName,
                        OperationContext.Current.IncomingMessageHeaders.Action);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                try
                {
                    Audit.AuthorizationFailed(userName,
                        OperationContext.Current.IncomingMessageHeaders.Action, "InstallSmartMeter method need Insert permission.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                
                DateTime time = DateTime.Now;
                string message = String.Format("Access is denied. User {0} try to call InstallSmartMeter method [time : {1}].\n" +
                    "For this method needs to have a \"Insert\" permission.", userName, time.TimeOfDay);


                SecurityException secEx = new SecurityException(message);
                throw new FaultException<SecurityException>(secEx, new FaultReason(secEx.Message));

            }
        }

      //  [PrincipalPermission(SecurityAction.Demand, Role = "Remove")]
        public void RemoveSmartMeter(int id)
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            string userName = Formatter.ParseName(principal.Identity.Name);

            if (Thread.CurrentPrincipal.IsInRole("Remove"))
            {
                string retVal = string.Empty;

                retVal = DataBase.DataBaseManager.DeleteEntity(id.ToString());

                if (!retVal.Equals(string.Empty))
                {
                    OperationException opEX = new OperationException(retVal);
                    throw new FaultException<OperationException>(opEX, new FaultReason(retVal));
                }

                try
                {
                    Audit.AuthorizationSuccess(userName,
                        OperationContext.Current.IncomingMessageHeaders.Action);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {


                try
                {
                    Audit.AuthorizationFailed(userName,
                        OperationContext.Current.IncomingMessageHeaders.Action, "RemoveSmartMeter() method need Remove permission.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                

                DateTime time = DateTime.Now;
                string message = String.Format("Access is denied. User {0} try to call RemoveSmartMeter() method [time : {1}].\n" +
                    "For this method needs to have a \"Remove\" permission.", userName, time.TimeOfDay);


                SecurityException secEx = new SecurityException(message);
                throw new FaultException<SecurityException>(secEx, new FaultReason(secEx.Message));

            }
        }


        

    }
}
