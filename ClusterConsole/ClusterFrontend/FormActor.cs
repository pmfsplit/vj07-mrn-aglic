using Akka.Actor;
using Akka.Cluster;
using Akka.Cluster.Tools.PublishSubscribe;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClusterFrontend
{
    class FormActor : ReceiveActor
    {
        List<IActorRef> actori = new List<IActorRef>();
        private Cluster cluster = Cluster.Get(Context.System);
        public FormActor()
        {
            Receive<Msg>(x => HandleMsg(x));

            Receive<ActorIdentity>(x => AddActor());

            Receive<ClusterEvent.MemberUp>(x => HandleMemberUp(x));
        }

        private void HandleMsg(Msg x)
        {
            throw new NotImplementedException();
        }

        private void AddActor()
        {
            actori.Add(Sender);
        }

        private void HandleMemberUp(ClusterEvent.MemberUp x)
        {
            var rootPath = new RootActorPath(x.Member.Address);
            var selection = Context.ActorSelection(rootPath);

            selection.Tell(new Identify("1"));
        }

        protected override void PreStart()
        {
            cluster.Subscribe(Self,
                new[] {
                    typeof(ClusterEvent.IMemberEvent),
                    typeof(ClusterEvent.UnreachableMember)
                });
            base.PreStart();
        }

        protected override void PostStop()
        {
            cluster.Unsubscribe(Self);
            base.PostStop();
        }
    }
}
