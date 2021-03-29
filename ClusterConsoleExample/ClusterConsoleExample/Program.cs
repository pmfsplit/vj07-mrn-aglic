using System;
using Akka.Actor;
using AkkaConfigProvider;
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
                Console.ReadLine();
                system.Terminate().Wait();
            }
        }
    }
}