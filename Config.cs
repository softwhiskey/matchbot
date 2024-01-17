using K4os.Compression.LZ4;
using Microsoft.Extensions.Configuration;

namespace Kinmatch
{
    internal static class Config
    {
        private static IConfigurationRoot? _configuration;

        static Config()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .Build();
        }
        //var ipsSection = _configuration.GetSection("Logging:LogLevel:ips");
        //var ips = ipsSection.GetChildren().Select(c => c.Value).ToArray();
        internal static class TelegramAPI
        {
            internal static string? token     => _configuration?["TelegramAPI:token"];
            internal static string? channelId => _configuration?["TelegramAPI:channelId"];
        }
        internal static class DB
        {
            internal static string? SERVER    => _configuration?["Database:SERVER"];
            internal static string? DATABASE  => _configuration?["Database:DATABASE"];
            internal static string? UID       => _configuration?["Database:UID"];
            internal static string? PASSWORD  => _configuration?["Database:PASSWORD"];
            internal static string? PORT      => _configuration?["Database:PORT"];
        }
    }
}
