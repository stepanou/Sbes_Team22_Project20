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
                Console.WriteLine("x x x x x x x x x x x x x x x x x x x x x x x x x x\n");
                Console.WriteLine("Security Error: {0}", e.Detail.Message);
                Console.WriteLine("x x x x x x x x x x x x x x x x x x x x x x x x x x\n");
            }
            catch (FaultException<OperationException> e)
            {
                Console.WriteLine("! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! !\n");
                Console.WriteLine("Operation Error: {0}", e.Detail.Message);
                Console.WriteLine("! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! !\n");
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
                Console.WriteLine("x x x x x x x x x x x x x x x x x x x x x x x x x x\n");
                Console.WriteLine("Security Error: {0}", e.Detail.Message);
                Console.WriteLine("x x x x x x x x x x x x x x x x x x x x x x x x x x\n");
            }
            catch (FaultException<OperationException> e)
            {
                Console.WriteLine("! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! !\n");
                Console.WriteLine("Operation Error: {0}", e.Detail.Message);
                Console.WriteLine("! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! !\n");
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
                Console.WriteLine("x x x x x x x x x x x x x x x x x x x x x x x x x x\n");
                Console.WriteLine("Security Error: {0}", e.Detail.Message);
                Console.WriteLine("x x x x x x x x x x x x x x x x x x x x x x x x x x\n");
            }
            catch (FaultException<OperationException> e)
            {
                Console.WriteLine("! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! !\n");
                Console.WriteLine("Operation Error: {0}", e.Detail.Message);
                Console.WriteLine("! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! !\n");
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
                Console.WriteLine("x x x x x x x x x x x x x x x x x x x x x x x x x x\n");
                Console.WriteLine("Security Error: {0}", e.Detail.Message);
                Console.WriteLine("x x x x x x x x x x x x x x x x x x x x x x x x x x\n");
            }
            catch (FaultException<OperationException> e)
            {
                Console.WriteLine("! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! !\n");
                Console.WriteLine("Operation Error: {0}", e.Detail.Message);
                Console.WriteLine("! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! !\n");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                this.Dispose();

            }
        }

        public string GetConsumption(int id, string clientConsumption)
        {

            try
            {

                return factory.GetConsumption(id, clientConsumption);

            }
            catch (FaultException<SecurityException> e)
            {
                Console.WriteLine("x x x x x x x x x x x x x x x x x x x x x x x x x x\n");
                Console.WriteLine("Security Error: {0}", e.Detail.Message);
                Console.WriteLine("x x x x x x x x x x x x x x x x x x x x x x x x x x\n");
                return string.Format("Security Error: {0}", e.Detail.Message);
            }
            catch (FaultException<OperationException> e)
            {
                Console.WriteLine("! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! !\n");
                Console.WriteLine("Operation Error: {0}", e.Detail.Message);
                Console.WriteLine("! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! !\n");
                return string.Format("Operation Error: {0}", e.Detail.Message);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                this.Dispose();
                return ex.Message;
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
                Console.WriteLine("x x x x x x x x x x x x x x x x x x x x x x x x x x\n");
                Console.WriteLine("Security Error: {0}", e.Detail.Message);
                Console.WriteLine("x x x x x x x x x x x x x x x x x x x x x x x x x x\n");
            }
            catch (FaultException<OperationException> e)
            {
                Console.WriteLine("! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! !\n");
                Console.WriteLine("Operation Error: {0}", e.Detail.Message);
                Console.WriteLine("! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! !\n");
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
                Console.WriteLine("x x x x x x x x x x x x x x x x x x x x x x x x x x\n");
                Console.WriteLine("Security Error: {0}", e.Detail.Message);
                Console.WriteLine("x x x x x x x x x x x x x x x x x x x x x x x x x x\n");
            }
            catch (FaultException<OperationException> e)
            {
                Console.WriteLine("! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! !\n");
                Console.WriteLine("Operation Error: {0}", e.Detail.Message);
                Console.WriteLine("! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! !\n");
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
