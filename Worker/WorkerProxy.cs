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
           
            factory = this.CreateChannel();
        }

        public WorkerProxy(InstanceContext instance,NetTcpBinding netTcp,EndpointAddress endpoint): base(instance,netTcp,endpoint)
        {
            string cltCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
            
            
            Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom;
            Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new CertValidator();
            Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;
            Credentials.ClientCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, cltCertCN);
            
            factory = this.CreateChannel();
        }


        
        public void RegisterWorker(string workerID)
        {
            try
            {

                factory.RegisterWorker(workerID);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

                
            }
        }

        public void UnregisterWorker(string workerID)
        {
            try
            {

                factory.UnregisterWorker(workerID);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

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
