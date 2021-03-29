using Akka.Actor;
using Akka.Routing;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClusterRouterExample
{
    class BackendConnActor : ReceiveActor
    {
        private IActorRef router = Context.ActorOf(
            Props.Empty.WithRouter(FromConfig.Instance), "backendrouter"
            );
        public BackendConnActor()
        {
            Receive<Nums>(nums => SendJobs(nums));
        }
        private void SendJobs(Nums nums)
        {
            foreach(var el in nums.Args)
            {
                router.Tell(new Job(el));
            }
        }
    }
}
