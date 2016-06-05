using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClusterManager
{
    // Master machine for managing worker machines
    public class Master
    {
        public int Id { get; private set; }
        List<Worker> Workers;
        Message message;

        public Master(Message messageSystem)
        {
            Id = 0;
            message = messageSystem;
            Workers = new List<Worker> ();
        }

        public int AddWorkers(IEnumerable<Worker> listOfMachines)
        {
            int nubmerOfAddedWorkers = 0;
            foreach (var worker in listOfMachines)
            {
                if (worker.State == WorkerState.Unreserved)
                {
                    message.Send(Id, MessageDescription.Reserve.ToString(), worker.Id);
                    int senderId;
                    string result;
                    message.Receive(Id, out result, out senderId);
                    if (result == MessageDescription.ReservedGranted.ToString())
                    {
                        Workers.Add(worker);
                        nubmerOfAddedWorkers++;
                    }
                }
            }
            return nubmerOfAddedWorkers;
        }

        public int RemoveWorkers(IEnumerable<Worker> listOfMachines)
        {
            int numberOfRemovedWorkers = 0;
            foreach (var worker in listOfMachines)
            {
                if (worker.State == WorkerState.Idle)
                {
                    Workers.Remove(worker);
                    if (message.Send(Id, MessageDescription.Free.ToString(), worker.Id))
                    {
                        numberOfRemovedWorkers++;
                    }
                    else
                        Workers.Add(worker);
                }
            }
            return numberOfRemovedWorkers;
        }
    }
}
