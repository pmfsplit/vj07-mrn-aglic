using System;
using Akka.Actor;
using Messages;

namespace ClusterBackend
{
    public class WorkerActor : ReceiveActor
    {
        public WorkerActor()
        {
            Receive<SaveToDatabase>(msg => Save(msg));
        }

        private void Save(SaveToDatabase msg)
        {
            Console.WriteLine($"{DateTime.Now} saved to DB {msg.Content}");
            Sender.Tell(new SaveToDatabaseAck(msg.Id));
        }
    }
}