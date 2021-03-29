using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public class WorkerActor : ReceiveActor
    {
        public WorkerActor()
        {
            Receive<Job>(x => Console.WriteLine($"{Self} Got {x.Num} from {Sender}"));
        }
    }
}
