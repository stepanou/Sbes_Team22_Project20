using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.ServiceModel;
using System.Collections.Concurrent;

namespace Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class LoadBalancer : ILoadBalancer, ICalculatePrice
    {

        private  Dictionary<string, ICallBackContract> numberOfWorkers = new Dictionary<string, ICallBackContract>();
        private List<string> Id = new List<string>();
        private Random random = new Random();

        public string CalculatePrice(string consumption)
        {
            if (numberOfWorkers.Count < 1)
            {
                return "There is no available resorces for calculation.";
            }

           // Random random = new Random();
            //random.Next(0, Id.Count - 1);

            string[] worker = Id.ToArray();
            string retVal = string.Empty;

            // numberOfWorkers[worker[random.Next(0, worker.Length - 1)]].DoWork(consumption);
            retVal = numberOfWorkers[worker[0]].DoWork(consumption);
            return retVal;    

        }

        public void RegisterWorker(string workerID)
        {
            if (!this.numberOfWorkers.ContainsKey(workerID))
            {
                this.numberOfWorkers.Add(workerID, OperationContext.Current.GetCallbackChannel<ICallBackContract>());
                Id.Add(workerID);
                Console.WriteLine($"{workerID} registered");
            }
            else
            {
                Console.WriteLine($"{workerID} already exists");
            }
        }

        public void UnregisterWorker(string workerID)
        {
            if (this.numberOfWorkers.ContainsKey(workerID))
            {
                this.numberOfWorkers.Remove(workerID);
                Id.Remove(workerID);
                Console.WriteLine($"{workerID} Unregistered");
            }
            else
            {
                Console.WriteLine($"{workerID} does not exist");
            }
        }
    }
}
