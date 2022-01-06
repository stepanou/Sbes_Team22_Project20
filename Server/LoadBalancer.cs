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

        private Dictionary<string, ICallBackContract> freeWorkers = new Dictionary<string, ICallBackContract>();
        private Dictionary<string, ICallBackContract> busyWorkers = new Dictionary<string, ICallBackContract>();

        public string CalculatePrice(string consumption)
        {
            if (freeWorkers.Count < 1)
            {
                return "There is no available resorces for calculation.";
            }

            string retVal = string.Empty;

            KeyValuePair<string, ICallBackContract> worker = freeWorkers.First();
            freeWorkers.Remove(worker.Key);

            busyWorkers.Add(worker.Key, worker.Value);

            retVal = worker.Value.DoWork(consumption);
            busyWorkers.Remove(worker.Key);
            freeWorkers.Add(worker.Key, worker.Value);


            return retVal;

        }

        public void RegisterWorker(string workerID)
        {
            if (!this.freeWorkers.ContainsKey(workerID) && !this.busyWorkers.ContainsKey(workerID))
            {
                this.freeWorkers.Add(workerID, OperationContext.Current.GetCallbackChannel<ICallBackContract>());

                Console.WriteLine($"{workerID} registered");
            }
            else
            {
                Console.WriteLine($"{workerID} already exists");
            }
        }

        public void UnregisterWorker(string workerID)
        {
            if (this.freeWorkers.ContainsKey(workerID) || this.busyWorkers.ContainsKey(workerID))
            {
                this.freeWorkers.Remove(workerID);

                Console.WriteLine($"{workerID} Unregistered");
            }
            else
            {
                Console.WriteLine($"{workerID} does not exist");
            }
        }
    }
}
