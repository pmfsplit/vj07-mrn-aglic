using Akka.Actor;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClusterRouterExample
{
    class FormActor : ReceiveActor
    {
        private IActorRef connActor;
        public FormActor()
        {
            connActor = 
                Context.ActorOf(Props.Create(() => new BackendConnActor()), "connector");
            Receive<Nums>(x => HandleNums(x));
        }

        private void HandleNums(Nums nums)
        {
            connActor.Tell(nums);
        }
    }
}
