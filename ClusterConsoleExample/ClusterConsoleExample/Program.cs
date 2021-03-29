using System;
using System.Threading;
using Akka.Actor;
using AkkaConfigProvider;
using Messages;
using SharedConfig;

namespace ClusterConsoleExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var configProvider = new ConfigProvider();
            var akkaConfig = configProvider.GetAkkaConfig<MyAkkaConfig>();

            using (var system = ActorSystem.Create("ClusterSystem", akkaConfig))
            {
                var props = Props.Create(() => new ConnectionActor());
                var actor = system.ActorOf(props);

                Console.ReadLine();
                actor.Tell(new SaveToDatabase(0, "Spremi ovo u bazu"));
                actor.Tell(new SaveToDatabase(1, "Aj spremi i ovo"));
                                    
                Console.ReadLine();
                system.Terminate().Wait();
            }
        }
    }
}