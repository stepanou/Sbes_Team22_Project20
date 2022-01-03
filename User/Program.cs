﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace User
{
    class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:12012/MainService";

            binding.Security.Mode = SecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign; // za Mode.Message ProtectionLevel ne postoji, why?


            EndpointAddress endpointAddress = new EndpointAddress(new Uri(address));//,
                                                                                    //   EndpointIdentity.CreateUpnIdentity("wcfServer"));

            int operation = 0;
            int id = 0;
            string consumption = string.Empty;
            string consumer = string.Empty;


            using (UserProxy proxy = new UserProxy(binding,endpointAddress))
            {
                do
                {
                    Menu();
                    Console.Write("Choose operation>> ");
                    operation = Int32.Parse(Console.ReadLine());
                   // Console.WriteLine("\n");

                    switch (operation)
                    {
                        case 1:
                            {
                                break;
                            }
                        case 2:
                            {
                                break;
                            }
                        case 3:
                            {
                                break;
                            }
                        case 4:
                            {
                                Console.WriteLine("4. Install new SmartMeter");
                                Console.Write("\tID= ");
                                id = Int32.Parse(Console.ReadLine());

                                Console.Write("\tConsumer= ");
                                consumer = Console.ReadLine();

                                Console.Write("\tConsumption= ");
                                consumption = Console.ReadLine();

                                proxy.InstallSmartMeter(id, consumer, consumption);

                                break;
                            }
                        case 5:
                            {
                                break;
                            }
                        case 6:
                            {
                                break;
                            }
                        case 7:
                            {
                                break;
                            }

                        default:
                            break;
                    }

                } while ((operation > 0) && (operation < 8)); 
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
