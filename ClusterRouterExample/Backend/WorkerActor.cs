using Akka.Actor;
using Shared;
using System;

namespace Backend
{
    class WorkerActor : ReceiveActor
    {
        public WorkerActor()
        {
            Receive<Job>(x => Console.WriteLine($"Got {x.Num} from {Sender}"));
        }
    }
}
