using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using SecurityManager;
using System.Security.Principal;
using System.Configuration;

namespace User
{
    class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            binding.CloseTimeout = new TimeSpan(0, 10, 0);
            binding.SendTimeout = new TimeSpan(0, 10, 0);
            binding.ReceiveTimeout = new TimeSpan(0, 10, 0);
            binding.OpenTimeout = new TimeSpan(0, 10, 0);


            string address = "net.tcp://localhost:12012/MainService";

            binding.Security.Mode = SecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;


            EndpointAddress endpointAddress = new EndpointAddress(new Uri(address));//,
                                                                                    //   EndpointIdentity.CreateUpnIdentity("wcfServer"));
           
            string keyFile = Formatter.ParseName(WindowsIdentity.GetCurrent().Name) + ".txt";
            string eSecretKey = SecretKey.GenerateKey();
            SecretKey.StoreKey(eSecretKey, ConfigurationManager.AppSettings["SecretKeyDirectory"],keyFile); 


            int operation = 0;
            int id = 0;
            int newId = 0;
            string dataForEncryption = string.Empty;
            const string separator = ";";
            string[] tempStr = new string[3];
            byte[] cipherText = null;

            using (UserProxy proxy = new UserProxy(binding,endpointAddress))
            {
                do
                {
                    Menu();
                    Console.Write("Choose operation>> ");

                    if (!Int32.TryParse(Console.ReadLine(), out operation))
                    {
                        Console.WriteLine("Wrong Input! Insert number from 1 to 8");
                        continue;
                    }
                    if (operation == 8)
                    {
                        break;
                    }
                    switch (operation)
                    {
                        case 1:
                            {
                                Console.WriteLine("1. Calculate electricity consumption price");
                                Console.Write("\tID= ");
                                Int32.TryParse(Console.ReadLine(),out id);
                                tempStr[0] = id.ToString();

                                Console.Write("\tConsumption= ");
                                
                                tempStr[1] = Console.ReadLine();
                                dataForEncryption = string.Join(separator, tempStr);


                                 cipherText = AES_Symm_Algorithm.EncryptFile(dataForEncryption, eSecretKey);

                                Console.WriteLine(proxy.GetConsumption(cipherText));
                                break;
                            }
                        case 2:
                            {
                                Console.WriteLine("2. Change SmartMeter's ID number");
                                Console.Write("\tID= ");
                                id = Int32.Parse(Console.ReadLine());
                                tempStr[0] = id.ToString();

                                Console.Write("\tNew ID= ");
                                newId = Int32.Parse(Console.ReadLine());
                                tempStr[1] = newId.ToString();

                                dataForEncryption = string.Join(separator, tempStr);
                                cipherText = AES_Symm_Algorithm.EncryptFile(dataForEncryption, eSecretKey);

                                proxy.ChangeSmartMeterID(cipherText);
                                break;
                            }
                        case 3:
                            {
                                Console.WriteLine("3. Change clients electricity consumption");
                                Console.Write("\tID= ");
                                id = Int32.Parse(Console.ReadLine());
                                tempStr[0] = id.ToString();

                                Console.Write("\tConsumption= ");
                                tempStr[1] = Console.ReadLine();

                                dataForEncryption = string.Join(separator, tempStr);
                                cipherText = AES_Symm_Algorithm.EncryptFile(dataForEncryption, eSecretKey);

                                proxy.ChangeClientsConsumption(cipherText);
                                break;
                            }
                        case 4:
                            {
                                Console.WriteLine("4. Install new SmartMeter");
                                Console.Write("\tID= ");
                                id = Int32.Parse(Console.ReadLine());
                                tempStr[0] = id.ToString();

                                Console.Write("\tConsumer= ");
                                tempStr[1] = Console.ReadLine();

                                Console.Write("\tConsumption= ");
                                tempStr[2] = Console.ReadLine();

                                dataForEncryption = string.Join(separator, tempStr);
                                cipherText = AES_Symm_Algorithm.EncryptFile(dataForEncryption, eSecretKey);

                                proxy.InstallSmartMeter(cipherText);
                                break;
                            }
                        case 5:
                            {
                                Console.WriteLine("5. Remove SmartMeter");
                                Console.Write("\tID= ");
                                id = Int32.Parse(Console.ReadLine());
                                tempStr[0] = id.ToString();

                                dataForEncryption = string.Join(separator, tempStr);
                                cipherText = AES_Symm_Algorithm.EncryptFile(dataForEncryption, eSecretKey);

                                proxy.RemoveSmartMeter(cipherText);
                                break;
                            }
                        case 6:
                            {
                                Console.WriteLine("6. Erase all entitys from  DataBase");
                                proxy.DeleteDataBase();
                                break;
                            }
                        case 7:
                            {
                                Console.WriteLine("7. Make an archive of current DataBase");
                                proxy.ArchiveDataBase();
                                break;
                            }

                        default:
                            break;
                    }

                } while (true); 
            }


            Console.ReadLine();

        }

        static void Menu()
        {

            Console.WriteLine("================*Operation Menu*================");
            Console.WriteLine("1. Calculate electricity consumption price");
            Console.WriteLine("2. Change SmartMeter's ID number");
            Console.WriteLine("3. Change clients electricity consumption");
            Console.WriteLine("4. Install new SmartMeter");
            Console.WriteLine("5. Remove SmartMeter");
            Console.WriteLine("6. Erase all entitys from  DataBase");
            Console.WriteLine("7. Make an archive of current DataBase");
            Console.WriteLine("8. Exit");
            Console.WriteLine("===================================================");
            
        }




    }
}
