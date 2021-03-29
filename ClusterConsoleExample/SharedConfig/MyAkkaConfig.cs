using System.Collections.Generic;
using Newtonsoft.Json;

namespace SharedConfig
{
    public class MyAkkaConfig
    {
        [JsonProperty(PropertyName = "actor")]
        public ActorConfig Actor { get; set; }
        [JsonProperty(PropertyName = "remote")]
        public RemoteConfig Remote { get; set; }
        [JsonProperty(PropertyName = "cluster")]
        public ClusterConfig Cluster { get; set; }
        public class ActorConfig
        {
            [JsonProperty(PropertyName = "provider")]
            public string Provider { get; set; }
        }
        public class ClusterConfig
        {
            [JsonProperty(PropertyName = "seed-nodes")]
            public List<string> SeedNodes { get; set; }
        }
        public class RemoteConfig
        {
            [JsonProperty(PropertyName = "dot-netty")]
            public DotNettyConfig DotNetty { get; set; }
            public class DotNettyConfig
            {
                [JsonProperty(PropertyName = "tcp")]
                public TcpConfig Tcp { get; set; }
                public class TcpConfig
                {
                    [JsonProperty(PropertyName = "port")]
                    public int Port { get; set; }
                    [JsonProperty(PropertyName = "hostname")]
                    public string Hostname { get; set; }
                }
            }
        }
    }
}