using System.Collections.Generic;
using Akka.Actor;
using Akka.Cluster;

namespace ClusterConsoleExample
{
    public class ConnectionActor : ReceiveActor
    {
        private List<IActorRef> _actorRefs;
        private Cluster _cluster = Cluster.Get(Context.System);

        public ConnectionActor()
        {
            _actorRefs = new List<IActorRef>();

            Receive<ClusterEvent.MemberUp>(up => SendIdentify(up));
            Receive<ActorIdentity>(identity => AddActor());
        }

        private void SendIdentify(ClusterEvent.MemberUp up)
        {
            var rootPath = new RootActorPath(up.Member.Address);
            var selection = Context.ActorSelection(rootPath);

            selection.Tell(new Identify("1"));
        }

        private void AddActor()
        {
            _actorRefs.Add(Sender);
        }

        protected override void PreStart()
        {
            _cluster.Subscribe(Self, ClusterEvent.SubscriptionInitialStateMode.InitialStateAsEvents,
                new[]
                {
                    typeof(ClusterEvent.IMemberEvent)
                });
            base.PreStart();
        }

        protected override void PostStop()
        {
            _cluster.Unsubscribe(Self);
            base.PostStop();
        }
    }
}