using KittyBot;
using KittyBot.Network;

namespace KittyBot
{
    internal class Program
    {
        public static string Token { get; private set; } = string.Empty;
        public static string Id { get; private set; } = string.Empty;
        public static string PublicKey { get; private set; } = string.Empty;
        public static string URL { get; private set; } = string.Empty;
        public static string Version { get; private set; } = string.Empty;

        private static void Main(string[] args) {
            string[] _envTokens = File.ReadAllLines(Path.Combine("./", ".env"));

            Token = _envTokens[0].Split("=")[1];
            Id = _envTokens[1].Split("=")[1];
            PublicKey = _envTokens[2].Split("=")[1];
            URL = _envTokens[3].Split("=")[1];
            Version = _envTokens[4].Split("=")[1];

            new Bot().Start();

        }
    }
}