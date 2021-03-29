using Akka.Actor;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClusterConsole
{
    class WorkerActor : ReceiveActor
    {
        public WorkerActor()
        {
            Receive<Msg>(x => HandleSaveToDatabase(x));
        }

        private void HandleSaveToDatabase(Msg msg)
        {
            Console.WriteLine(msg.Text);
        }
    }
}
