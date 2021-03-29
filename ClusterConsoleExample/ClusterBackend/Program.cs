using System;
using AkkaConfigProvider;
using SharedConfig;

namespace ClusterBackend
{
    class Program
    {
        static void Main(string[] args)
        {
            var configProvider = new ConfigProvider();
            var akkaConfig = configProvider.GetAkkaConfig<MyAkkaConfig>();

            Console.ReadLine();
        }
    }
}