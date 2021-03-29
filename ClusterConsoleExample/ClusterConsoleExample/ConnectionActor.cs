using System;
using System.Collections.Generic;
using Akka.Actor;
using Akka.Cluster;
using Messages;

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

            Receive<SaveToDatabase>(db => ForwardToAll(db));
            Receive<SaveToDatabaseAck>(db => HandleAck(db));
        }

        private void HandleAck(SaveToDatabaseAck db)
        {
            Console.WriteLine($"[{Sender}] Save to database: {db.Id}");
        }

        private void ForwardToAll(SaveToDatabase db)
        {
            foreach (var actorRef in _actorRefs)
            {
                actorRef.Tell(db);
            }
        }

        private void SendIdentify(ClusterEvent.MemberUp up)
        {
            if (up.Member.Address == _cluster.SelfMember.Address) return;
            var rootPath = new RootActorPath(up.Member.Address);
            var selection = Context.ActorSelection($"{rootPath}/user/*");

            selection.Tell(new Identify("1"));
        }

        private void AddActor()
        {
            Console.WriteLine($"Got response from {Sender}");
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