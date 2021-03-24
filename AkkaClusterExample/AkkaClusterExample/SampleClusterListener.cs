using System;
using System.Collections.Generic;
using Akka.Actor;
using Akka.Cluster;
using Akka.Event;

namespace AkkaClusterExample
{
    class SampleClusterListener : ReceiveActor
    {
        private Cluster Cluster = Cluster.Get(Context.System);
        private ILoggingAdapter Log = Context.GetLogger();

        private Dictionary<Address, int> unreachableCount;

        protected override void PreStart()
        {
            /// Pretplatit cemo ovog actora na promjene u stanju cvorova actor sustava 
            Cluster.Subscribe(Self,
                // dobij trenutno stanje kao stream događaj
                // Actor će dobiti stream MemberUp, MemberDown i drugih događaja
                ClusterEvent.SubscriptionInitialStateMode.InitialStateAsEvents, 
                // Actor će dobiti trenutno stanje klastera u poruci ClusterEvent.ClusterState.
                // Ta poruka će opisati sve članov klastera, uloge i membership status
                // ClusterEvent.SubscriptionInitialStateMode.InitialStateAsSnapshot 
                new[]{
                    typeof(ClusterEvent.IReachabilityEvent),
                    typeof(ClusterEvent.IMemberEvent)
                });
        }

        protected override void PostStop()
        {
            Cluster.Unsubscribe(Self);
        }

        public SampleClusterListener()
        {
            // get the current state of cluster
            // Cluster.SendCurrentClusterState (Self);
            unreachableCount = new Dictionary<Address, int>();
            Receive<ClusterEvent.MemberUp>(x => HandleMemberUp(x));
            Receive<ClusterEvent.UnreachableMember>(x => HandleUnreachable(x));
            Receive<ClusterEvent.ReachableMember>(x => HandleReachable(x));
            Receive<ClusterEvent.MemberRemoved>(x => HandleMemberRemoved(x));
        }

        private void HandleReachable(ClusterEvent.ReachableMember reachable)
        {
            Log.Info("Member is reachable: {0}", reachable.Member);
        }

        private void HandleMemberUp(ClusterEvent.MemberUp up)
        {
            Log.Info("Member is up: {0}", up.Member);
        }

        private void HandleUnreachable(ClusterEvent.UnreachableMember unreachable)
        {
            var member = unreachable.Member;
            var address = member.Address;
            if (unreachableCount.ContainsKey(address))
            {
                unreachableCount[address]++;
            }
            else
            {
                unreachableCount.Add(address, 1);
            }

            if(unreachableCount[address] > 5)
            {
                Cluster.Down(member.Address);
            }
            Log.Info("Member detected as unreachable: {0}", member);
        }

        private void HandleMemberRemoved(ClusterEvent.MemberRemoved removed)
        {
            Log.Info("Member is removed: {0}", removed.Member);
        }

        protected override void Unhandled(object message)
        {
            //Log.Warning("Got unhandled message: {0}", message);
            base.Unhandled(message);
        }
    }
}
