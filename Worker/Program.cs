using SecurityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            string srvCertCN = "Server";//kako god damo sertifikatu za server naziv

            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, srvCertCN);
            EndpointAddress address = new EndpointAddress(new Uri("net.tcp://localhost:11012/LoadBalancer"), new X509CertificateEndpointIdentity(srvCert));

            CallbackContract workerContract = new CallbackContract();

            InstanceContext instanceContext = new InstanceContext(workerContract);


           // using (WorkerProxy workerProxy = new WorkerProxy(binding, address, workerContract))

            using (WorkerProxy workerProxy = new WorkerProxy(instanceContext,binding,address))
            {
                Console.WriteLine("Worker id: " + workerProxy.ID);

                int operation = 0;

                do
                {

                    Menu();
                    Console.Write("Choose operation >> ");
                    operation = int.Parse(Console.ReadLine());

                    switch (operation)
                    {

                        case 1:
                            {
                                workerProxy.RegisterWorker(workerProxy.ID);
                                break;
                            }
                        case 2:
                            {
                                workerProxy.UnregisterWorker(workerProxy.ID);
                                break;
                            }


                        default:
                            break;
                    }


                } while (operation > 0 || operation < 3);


                Console.ReadKey();
            }



        }
        public static void Menu()
        {
            Console.WriteLine("================*Operation Menu*================");
            Console.WriteLine("1. Register worker");
            Console.WriteLine("2. Unregister worker");
            Console.WriteLine("3. Exit");
            Console.WriteLine("===================================================");

        }
    }
}
