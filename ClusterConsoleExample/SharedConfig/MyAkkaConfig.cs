using Newtonsoft.Json;

namespace SharedConfig
{
    public class MyAkkaConfig
    {
        [JsonProperty(PropertyName = "actor")]
        public ActorConfig Actor { get; set; }
        public class ActorConfig
        {
         
            public class RemoteConfig
            {
                public class DotNettyConfig
                {
                    public class TcpConfig
                    {
                        
                    }
                }
            }
            
            public class Cluster
            {
                
            }
        }
    }
}