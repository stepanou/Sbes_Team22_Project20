using SecurityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;

namespace Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            string srvCertCN = "lignjoslav";//kako god damo sertifikatu za server naziv

            NetTcpBinding binding = new NetTcpBinding();
            binding.CloseTimeout = new TimeSpan(0, 10, 0);
            binding.SendTimeout = new TimeSpan(0, 10, 0);
            binding.ReceiveTimeout = new TimeSpan(0, 10, 0);
            binding.OpenTimeout = new TimeSpan(0, 10, 0);

            
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, srvCertCN);
            EndpointAddress address = new EndpointAddress(new Uri("net.tcp://localhost:11012/LoadBalancer"),new X509CertificateEndpointIdentity(srvCert));

            CallbackContract workerContract = new CallbackContract();

            InstanceContext instanceContext = new InstanceContext(workerContract);


           // using (WorkerProxy workerProxy = new WorkerProxy(binding, address, workerContract))

            using (WorkerProxy workerProxy = new WorkerProxy(instanceContext,binding,address))
            {
                Console.WriteLine("Worker id: " + workerProxy.ID);
                Console.WriteLine(WindowsIdentity.GetCurrent().Name);

                int operation = 0;

                do
                {
                    Menu();
                    Console.Write("Choose operation >> ");
                    if (!Int32.TryParse(Console.ReadLine(), out operation))
                    {
                        Console.WriteLine("Wrong Input! Insert number from 1 to 3.");
                        continue;
                    }
                    if (operation == 3)
                    {
                        workerProxy.UnregisterWorker(workerProxy.ID);
                        break;
                    }
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

                    

                    if (workerProxy.State.Equals(CommunicationState.Faulted))
                    {
                        break;
                    }

                } while (true);
            }

            Console.WriteLine("Press ENTER to close");
            Console.ReadKey();

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
