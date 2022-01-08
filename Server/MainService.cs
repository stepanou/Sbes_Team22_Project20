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
using System.Configuration;
using System.Text.RegularExpressions;

namespace Server
{
    public class MainService : IMainService
    {


     
        public void ArchiveDataBase()
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            string userName = Formatter.ParseName(principal.Identity.Name);

            if (Thread.CurrentPrincipal.IsInRole("Archive"))
            {
                string retVal = string.Empty;  
                
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

        
        public void ChangeClientsConsumption(byte[] idAndNewConsumption)
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            string userName = Formatter.ParseName(principal.Identity.Name);


            if (Thread.CurrentPrincipal.IsInRole("Modify"))
            {
                string sKey = SecretKey.LoadKey(ConfigurationManager.AppSettings["SecretKeyDirectory"] + userName + ".txt");
                string decryptedStr = AES_Symm_Algorithm.DecryptFile(idAndNewConsumption, sKey);
                string[] parameters = decryptedStr.Split(';');

                string retVal = string.Empty;

                retVal = DataBase.DataBaseManager.ModifiyEntity(parameters[0], parameters[1]);

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

        
        public void ChangeSmartMeterID(byte[] idAndNewId)
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            string userName = Formatter.ParseName(principal.Identity.Name);


            if (Thread.CurrentPrincipal.IsInRole("Modify"))
            {

                string sKey = SecretKey.LoadKey(ConfigurationManager.AppSettings["SecretKeyDirectory"] + userName + ".txt");
                string decryptedStr = AES_Symm_Algorithm.DecryptFile(idAndNewId, sKey);
                string[] parameters = decryptedStr.Split(';');
                
                string retVal = string.Empty;

                retVal = DataBase.DataBaseManager.ModifiyEntityId(parameters[0], parameters[1]);

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

        
        public string GetConsumption(byte[] idAndConsumption)
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            string userName = Formatter.ParseName(principal.Identity.Name);


            if (Thread.CurrentPrincipal.IsInRole("Read"))
            {

                string sKey = SecretKey.LoadKey(ConfigurationManager.AppSettings["SecretKeyDirectory"] + userName + ".txt");
                string decryptedStr = AES_Symm_Algorithm.DecryptFile(idAndConsumption, sKey);
                string[] parameters = decryptedStr.Split(';');



                string retVal = string.Empty;
                SmartMeter smartMeter = null;

                smartMeter = DataBase.DataBaseManager.GetEntity(parameters[0].ToString());

                if (smartMeter == null)
                {
                    OperationException opEX = new OperationException("SmartMeter does not exist");
                    throw new FaultException<OperationException>(opEX, new FaultReason("SmartMeter does not exist"));
                }

                var cultureInfo = CultureInfo.InvariantCulture;
                // if the first regex matches, the number string is in us culture
                if (Regex.IsMatch(parameters[1], @"^(:?[\d,]+\.)*\d+$"))
                {
                    cultureInfo = new CultureInfo("en-US");
                }
                // if the second regex matches, the number string is in de culture
                else if (Regex.IsMatch(parameters[1], @"^(:?[\d.]+,)*\d+$"))
                {
                    cultureInfo = new CultureInfo("de-DE");
                }
                NumberStyles styles = NumberStyles.Number;

                double amount = 0;
                bool isDouble = double.TryParse(parameters[1], styles, cultureInfo, out amount);


                smartMeter.Consumption += amount;

                retVal = DataBase.DataBaseManager.ModifiyEntity(smartMeter.Id.ToString(),smartMeter.Consumption.ToString("0.000"));

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

                string address = "net.tcp://localhost:11012/CalculatePrice";
                ChannelFactory<ICalculatePrice> channelFactory = new ChannelFactory<ICalculatePrice>(new NetTcpBinding(), new EndpointAddress(new Uri(address)));
                ICalculatePrice proxy = channelFactory.CreateChannel();

                retVal = proxy.CalculatePrice(parameters[1]);
                return retVal;

            }
            else
            {
                try
                {
                    Audit.AuthorizationFailed(userName,
                        OperationContext.Current.IncomingMessageHeaders.Action, "GetConsumption method need Read permission.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                DateTime time = DateTime.Now;
                string message = String.Format("Access is denied. User {0} try to call GetConsumption [time : {1}].\n" +
                    "For this method needs to have a \"Read\" permission.", userName, time.TimeOfDay);


                SecurityException secEx = new SecurityException(message);
                throw new FaultException<SecurityException>(secEx, new FaultReason(secEx.Message));

            }


        }

      
        public void InstallSmartMeter(byte[] idUserConsumption)
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            string userName = Formatter.ParseName(principal.Identity.Name);


            if (Thread.CurrentPrincipal.IsInRole("Insert"))
            {
                string sKey = SecretKey.LoadKey(ConfigurationManager.AppSettings["SecretKeyDirectory"] + userName + ".txt");
                string decryptedStr = AES_Symm_Algorithm.DecryptFile(idUserConsumption, sKey);
                string[] parameters = decryptedStr.Split(';');

                string retVal = string.Empty;

                retVal = DataBase.DataBaseManager.InsertEntity(parameters[0], parameters[1], parameters[2]);

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
        public void RemoveSmartMeter(byte[] id)
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            string userName = Formatter.ParseName(principal.Identity.Name);

            if (Thread.CurrentPrincipal.IsInRole("Remove"))
            {
                string sKey = SecretKey.LoadKey(ConfigurationManager.AppSettings["SecretKeyDirectory"] + userName + ".txt");
                string decryptedStr = AES_Symm_Algorithm.DecryptFile(id, sKey);
                string[] parameters = decryptedStr.Split(';');

                string retVal = string.Empty;

                retVal = DataBase.DataBaseManager.DeleteEntity(parameters[0]);

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
