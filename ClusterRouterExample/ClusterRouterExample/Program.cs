using Akka.Actor;
using Akka.Configuration;
using Akka.Configuration.Hocon;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace ClusterRouterExample
{
    static class Program
    {
        public static ActorSystem System { get; private set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var appConfig =(AkkaConfigurationSection)ConfigurationManager.GetSection("akka");
            var roleConfig = ConfigurationFactory.ParseString("akka.cluster.roles=[frontend]");
            var config = roleConfig.WithFallback(appConfig.AkkaConfig);

            System = ActorSystem.Create("ClusterRouterExample", config);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
