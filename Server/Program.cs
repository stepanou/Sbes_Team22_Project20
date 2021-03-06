using System;
using System.Collections.Generic;
using System.IdentityModel.Policy;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using Common;
using SecurityManager;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            string srvCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            NetTcpBinding binding = new NetTcpBinding();

            binding.CloseTimeout = new TimeSpan(0, 10, 0);
            binding.SendTimeout = new TimeSpan(0, 10, 0);
            binding.ReceiveTimeout = new TimeSpan(0, 10, 0);
            binding.OpenTimeout = new TimeSpan(0, 10, 0);

            string address = "net.tcp://localhost:12012/MainService";

            ServiceHost host = new ServiceHost(typeof(MainService));
            host.AddServiceEndpoint(typeof(IMainService), binding, address);

            // podesavamo custom polisu, odnosno nas objekat principala
            host.Authorization.PrincipalPermissionMode = PrincipalPermissionMode.Custom;
            List<IAuthorizationPolicy> policies = new List<IAuthorizationPolicy>();
            policies.Add(new CustomAuthorizationPolicy());
            host.Authorization.ExternalAuthorizationPolicies = policies.AsReadOnly();

            // deo za auditing
            ServiceSecurityAuditBehavior newAudit = new ServiceSecurityAuditBehavior();
            newAudit.AuditLogLocation = AuditLogLocation.Application;
            newAudit.ServiceAuthorizationAuditLevel = AuditLevel.SuccessOrFailure;

            host.Description.Behaviors.Remove<ServiceSecurityAuditBehavior>();
            host.Description.Behaviors.Add(newAudit);

            NetTcpBinding binding2 = new NetTcpBinding();
            binding2.CloseTimeout = new TimeSpan(0, 10, 0);
            binding2.SendTimeout = new TimeSpan(0, 10, 0);
            binding2.ReceiveTimeout = new TimeSpan(0, 10, 0);
            binding2.OpenTimeout = new TimeSpan(0, 10, 0);
            binding2.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            string address2 = "net.tcp://localhost:11012/LoadBalancer";

            NetTcpBinding binding3 = new NetTcpBinding();
            binding3.CloseTimeout = new TimeSpan(0, 10, 0);
            binding3.SendTimeout = new TimeSpan(0, 10, 0);
            binding3.ReceiveTimeout = new TimeSpan(0, 10, 0);
            binding3.OpenTimeout = new TimeSpan(0, 10, 0);


            string address3 = "net.tcp://localhost:11012/CalculatePrice";

            ServiceHost host2 = new ServiceHost(typeof(LoadBalancer));

            host2.AddServiceEndpoint(typeof(ILoadBalancer), binding2, address2);
            host2.AddServiceEndpoint(typeof(ICalculatePrice), binding3, address3);

            
            host2.Credentials.ClientCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom;
            host2.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;
            host2.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new CertValidator();
            host2.Credentials.ServiceCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, srvCertCN);

            
            try
            {
                host.Open();
                Console.WriteLine("SmartMeter Service is opened.");
                host2.Open();
                Console.WriteLine("LoadBalancer Service is opened.");
                Console.WriteLine("Press <enter> to finish...");

                Console.ReadLine();
            }
            catch (Exception e)
            {

                Console.WriteLine("[ERROR] {0}", e.Message);
                Console.WriteLine("[StackTrace] {0}", e.StackTrace);
            }
            finally
            {
                host2.Close();
                host.Close();
            }
        }
    }
}
