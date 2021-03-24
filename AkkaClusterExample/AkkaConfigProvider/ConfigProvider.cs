using System;
using System.IO;
using Akka.Configuration;
using Microsoft.Extensions.Configuration;

namespace AkkaConfigProvider
{
    public class ConfigProvider
    {
        private readonly IConfiguration _configuration;
        public string BaseDir { get; }
        public ConfigProvider(string baseDir, string filename)
        {
            _configuration = GetConfiguration(filename, baseDir);
        }

        public ConfigProvider(string filename)
        {
            string baseDir = GetBaseDir();
            _configuration = GetConfiguration(filename, baseDir);
        }

        public ConfigProvider()
        {
            string baseDir = GetBaseDir();
            string defaultFilename = "appsettings.json";
            _configuration = GetConfiguration(defaultFilename, baseDir);
        }

        private IConfiguration GetConfiguration(string filename, string baseDir)
        {
            return new ConfigurationBuilder()
                .SetBasePath(baseDir)
                .AddJsonFile($"{baseDir}/{filename}")
                .Build();
        }

        private string GetBaseDir()
        {
            var currDir = Directory.GetCurrentDirectory();
            var lastIndex = currDir.LastIndexOf("bin", StringComparison.InvariantCultureIgnoreCase);
            var projectDir =
                lastIndex < 0
                    ? currDir
                    : currDir.Substring(0, currDir.LastIndexOf("bin", StringComparison.InvariantCultureIgnoreCase));

            return projectDir;
        }

        private T MapAkkaConfig<T>()
        {
            return _configuration.GetSection("akka").Get<T>();
        }

        public Config GetAkkaConfig<T>()
        {
            T akkaConfig = MapAkkaConfig<T>();

            var fullConfig = new {akka = akkaConfig};
            return ConfigurationFactory.FromObject(fullConfig);
        }
    }
}