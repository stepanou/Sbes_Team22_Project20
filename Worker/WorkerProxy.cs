using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Worker
{
    public class WorkerProxy : DuplexChannelFactory<ILoadBalancer>, ILoadBalancer, IDisposable
    {

        ILoadBalancer factory;
        public string ID = Guid.NewGuid().ToString();


        public WorkerProxy(NetTcpBinding binding, EndpointAddress address, CallbackContract callback): base(callback,binding,address)
        {

            //Za sertifikate...


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
