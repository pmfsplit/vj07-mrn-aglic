using Akka.Actor;
using Akka.Configuration;
using Akka.Configuration.Hocon;
using System;
using System.Configuration;

namespace Backend
{
    class Program
    {
        static void Main(string[] args)
        {
            var appConfig = (AkkaConfigurationSection)ConfigurationManager.GetSection("akka");
            var roleConfig = ConfigurationFactory.ParseString("akka.cluster.roles=[backend]");
            var config = roleConfig.WithFallback(appConfig.AkkaConfig);

            using (var system = ActorSystem.Create("ClusterRouterExample", config))
            {
                var props = Props.Create(() => new WorkerActor());
                system.ActorOf(props, "worker");

                Console.ReadLine();
            }
        }
    }
}
