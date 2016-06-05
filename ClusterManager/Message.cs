using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClusterManager
{
    public enum MessageDescription
    {
        Reserve,
        ReservedGranted,
        ReservedRejected,
        Free,
        Console_WriteLine,
        Execute
    };

    // Class Message is responsible for parsing message between workers and between master and worker
    public class Message
    {
        int SenderId;
        int ReceiverId;
        bool Received;
        string Data;
        bool sendBusy;

        public bool Send(int senderId, string data, int receiverId)
        {
            while (sendBusy) ;
            if (string.IsNullOrEmpty(data))
                return false;

            sendBusy = true;

            Received = false;
            SenderId = senderId;
            ReceiverId = receiverId;
            Data = data;

            while (!Received) ;
            sendBusy = false;

            return true;
        }

        public bool Receive(int receiverId, out string data, out int senderId)
        {
            data = null;
            senderId = -1;

            while (ReceiverId != receiverId) ;

            if (string.IsNullOrEmpty(Data))
                return false;

            senderId = SenderId;
            data = Data;
            Data = null;
            Received = true;

            return true;
        }
    }
}
