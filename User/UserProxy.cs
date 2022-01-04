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

        public void ArchiveDataBase()
        {
            try
            {

                factory.ArchiveDataBase();

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

        public void ChangeClientsConsumption(int id, string newConsumption)
        {
            try
            {

                factory.ChangeClientsConsumption(id, newConsumption);

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

        public void ChangeSmartMeterID(int id, int newID)
        {
            try
            {

                factory.ChangeSmartMeterID(id, newID);

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

        public void DeleteDataBase()
        {

            try
            {

                factory.DeleteDataBase();

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

        public float GetConsumption(int id, string clientConsumption)
        {

            try
            {

                return factory.GetConsumption(id, clientConsumption);

            }
            catch (FaultException<SecurityException> e)
            {
                Console.WriteLine("x x x x x x x x x x x x x x x x x x x x x x x x x x");
                Console.WriteLine("Security Error: {0}", e.Detail.Message);
                Console.WriteLine("x x x x x x x x x x x x x x x x x x x x x x x x x x");
                return 0;
            }
            catch (FaultException<OperationException> e)
            {
                Console.WriteLine("! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! !");
                Console.WriteLine("Operation Error: {0}", e.Detail.Message);
                Console.WriteLine("! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! !");
                return 0;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                this.Dispose();
                return 0;
            }
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

        public void RemoveSmartMeter(int id)
        {
            try
            {

                factory.RemoveSmartMeter(id);

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
