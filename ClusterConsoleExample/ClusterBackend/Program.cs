using System;
using Akka.Actor;
using Akka.Configuration;
using AkkaConfigProvider;
using SharedConfig;

namespace ClusterBackend
{
    class Program
    {
        static void Main(string[] args)
        {
            var port = args.Length == 0 ? 0 : int.Parse(args[0]);

            var configProvider = new ConfigProvider();
            var akkaConfig = configProvider.GetAkkaConfig<MyAkkaConfig>();

            var config = ConfigurationFactory.ParseString("akka.remote.dot-netty.tcp.port=" + port)
                .WithFallback(akkaConfig);

            using (var system = ActorSystem.Create("ClusterSystem", config))
            {
                var props = Props.Create(() => new WorkerActor());
                system.ActorOf(props);
                Console.ReadLine();
                system.Terminate().Wait();
            }
        }
    }
}