using Akka.Actor;
using Akka.Cluster;

namespace ClusterConsoleExample
{
    public class ConnectionActor : ReceiveActor
    {
        private Cluster _cluster = Cluster.Get(Context.System);

        public ConnectionActor()
        {
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