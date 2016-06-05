using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClusterManager
{
    public enum WorkerState
    {
        Unreserved,
        Idle,
        Busy
    };

    // Worker manipulated by master machine
    // Worker is doing task 
    public class Worker
    {
        public WorkerState State;
        public int Id { get; private set; }
        Message message;
        string data;
        bool terminate;
        Thread coreThread = new Thread(Core);

        public Worker(int workerId, Message messageSystem)
        {
            coreThread.Name = "Worker" + workerId;
            terminate = false;
            State = WorkerState.Unreserved;
            message = messageSystem;
            Id = workerId;
            coreThread.Start(this);
        }

        static void Core(object obj)
        {
            Worker worker = (Worker)obj;
            while (!worker.terminate)
            {
                int senderId;
                worker.message.Receive(worker.Id, out worker.data, out senderId);
                if (worker.data == MessageDescription.Reserve.ToString())
                {
                    if (worker.State == WorkerState.Unreserved)
                    {
                        worker.message.Send(worker.Id, MessageDescription.ReservedGranted.ToString(), senderId);
                        worker.State = WorkerState.Idle;
                    }
                    else
                    {
                        worker.message.Send(worker.Id, MessageDescription.ReservedRejected.ToString(), senderId);
                    }
                }
                if (worker.data == MessageDescription.Free.ToString())
                {
                    worker.State = WorkerState.Unreserved;
                }
            }
        }
    }
}
