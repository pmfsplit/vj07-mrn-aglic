using Akka.Actor;
using Akka.Cluster;
using Akka.Configuration;
using Akka.Configuration.Hocon;
using System;
using System.Configuration;
using AkkaConfigProvider;

namespace AkkaClusterExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var ports = args.Length == 0 ? new[] { "0" } : args;

            var configProvider = new ConfigProvider();
            var akkaConfig = configProvider.GetAkkaConfig<MyAkkaConfig>();
            // var section = (AkkaConfigurationSection)ConfigurationManager.GetSection("akka");

            // Ne mozemo pokrenuti svaki sustav na isti port:
            var config = ConfigurationFactory.ParseString("akka.remote.dot-netty.tcp.port=" + ports[0]).WithFallback(akkaConfig);// section.AkkaConfig za .net Framework

            using (var system = ActorSystem.Create("ClusterSystem", config))
            {
                var actor = system.ActorOf(Props.Create(() => new SampleClusterListener()), "clusterListener");
                Console.ReadLine();
            }
        }
    }
}