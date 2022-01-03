using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Common;

namespace User
{
    public class UserProxy : ChannelFactory<IMainService>, IMainService, IDisposable
    {

        IMainService factory = null;

        public UserProxy(NetTcpBinding binding, string address) : base(binding, address)
        {
            factory = this.CreateChannel();
        }

        public UserProxy(NetTcpBinding binding, EndpointAddress address) : base(binding, address)
        {

            Credentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;
            factory = this.CreateChannel();
            
        }

        public bool ArchiveDataBase()
        {
            bool retVal = false;

            try
            {

                return retVal;
            }
            catch (FaultException<SecurityException> e)
            {
                Console.WriteLine("Error while trying to Read : {0}", e.Detail.Message);
                return retVal;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return retVal;
            }
        }

        public bool ChangeClientsConsumption(int id, string newConsumption)
        {
            bool retVal = false;

            try
            {

                return retVal;
            }
            catch (FaultException<SecurityException> e)
            {
                Console.WriteLine("Error while trying to Read : {0}", e.Detail.Message);
                return retVal;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return retVal;
            }
        }

        public bool ChangeSmartMeterID(int id, int newID)
        {
            bool retVal = false;

            try
            {

                return retVal;
            }
            catch (FaultException<SecurityException> e)
            {
                Console.WriteLine("Error while trying to Read : {0}", e.Detail.Message);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return retVal;
            }
            return retVal;
        }

        public bool DeleteDataBase()
        {
            bool retVal = false;

            try
            {

                return retVal;
            }
            catch (FaultException<SecurityException> e)
            {
                Console.WriteLine("Error while trying to Read : {0}", e.Detail.Message);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return retVal;
            }
            return retVal;
        }

        public float GetConsumption(int id, string clientConsumption)
        {
            
            try
            {

                return 0;
            }
            catch (FaultException<SecurityException> e)
            {
                Console.WriteLine("Error while trying to Read : {0}", e.Detail.Message);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return 0;
            }
            return 0;
        }

        public void InstallSmartMeter(int id, string user, string consumption)
        {
            
            try
            {

                factory.InstallSmartMeter(id, user, consumption);

            }
            catch (FaultException<SecurityException> e)
            {
                Console.WriteLine("x x x x x x x x x x x x x x x x x x x x x x x x x x");
                Console.WriteLine("Security Error: {0}", e.Detail.Message);
                Console.WriteLine("x x x x x x x x x x x x x x x x x x x x x x x x x x");
            }
            catch (FaultException<OperationException> e)
            {
                Console.WriteLine("! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! !");
                Console.WriteLine("Operation Error: {0}", e.Detail.Message);
                Console.WriteLine("! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! !");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                this.Dispose();
               
            }
            
        }

        public bool RemoveSmartMeter(int id)
        {
            bool retVal = false;

            try
            {

                return retVal;
            }
            catch (FaultException<SecurityException> e)
            {
                Console.WriteLine("Error while trying to Read : {0}", e.Detail.Message);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return retVal;
            }
            return retVal;
        }
        public void Dispose()
        {
            if (factory != null)
            {
                factory = null;
            }
           
            this.Close();
        }
    }
}
