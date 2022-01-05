using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common;
using SecurityManager;

namespace Worker
{
    public class WorkerProxy : DuplexChannelFactory<ILoadBalancer>, ILoadBalancer, IDisposable
    {

        ILoadBalancer factory;
        public string ID = Guid.NewGuid().ToString();


        public WorkerProxy(NetTcpBinding binding, EndpointAddress address, CallbackContract callback): base(callback,binding,address)
        {
            string cltCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.ChainTrust;
            Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;
            Credentials.ClientCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, cltCertCN);

            factory = this.CreateChannel();
        }

        public WorkerProxy(InstanceContext instance,NetTcpBinding netTcp,EndpointAddress endpoint): base(instance,netTcp,endpoint)
        {


            factory = this.CreateChannel();
        }


        public void Dispose()
        {
            if (factory != null)
            {

                factory = null;
            }

            Close();
        }



        public void RegisterWorker(string workerID)
        {
            try
            {

                factory.RegisterWorker(workerID);

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

        public void UnregisterWorker(string workerID)
        {
            try
            {

                factory.UnregisterWorker(workerID);

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
    }
}
