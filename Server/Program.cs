using System;
using System.Collections.Generic;
using System.IdentityModel.Policy;
using System.Linq;
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
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:12012/MainService";

            ServiceHost host = new ServiceHost(typeof(MainService));
            host.AddServiceEndpoint(typeof(IMainService), binding, address);


            // podesavamo custom polisu, odnosno nas objekat principala
            host.Authorization.PrincipalPermissionMode = PrincipalPermissionMode.Custom;
            List<IAuthorizationPolicy> policies = new List<IAuthorizationPolicy>();
            policies.Add(new CustomAuthorizationPolicy());
            host.Authorization.ExternalAuthorizationPolicies = policies.AsReadOnly();

            // deo za auditing
            //ServiceSecurityAuditBehavior newAudit = new ServiceSecurityAuditBehavior();
            //newAudit.AuditLogLocation = AuditLogLocation.Application;
            //newAudit.ServiceAuthorizationAuditLevel = AuditLevel.SuccessOrFailure;

            //host.Description.Behaviors.Remove<ServiceSecurityAuditBehavior>();
            //host.Description.Behaviors.Add(newAudit);

            host.Open();
            Console.WriteLine("SmartMeter Service is opened. Press <enter> to finish...");

            NetTcpBinding binding2 = new NetTcpBinding();
            string address2 = "net.tcp://localhost:11012/LoadBalancer";
            ServiceHost host2 = new ServiceHost(typeof(LoadBalancer));
            host2.AddServiceEndpoint(typeof(ILoadBalancer), binding2, address2);
            host2.AddServiceEndpoint(typeof(ICalculatePrice), binding2, address2);

            host2.Open();
            Console.WriteLine("LoadBalancer Service is opened. Press <enter> to finish...");

            Console.ReadLine();

            host.Close();
        }
    }
}
