using System.Text;
using KittyBot;
using KittyBot.Network;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

            Payload handShake = new Payload();
            handShake.op = (int)GatewayOpCodes.Identify;
            handShake.d = new JObject {
                { "token", Program.Token },
                { "intents", Intents.GUILD_MESSAGE_REACTIONS | Intents.GUILD_MESSAGES | Intents.GUILD_MEMBERS },
                { "properties", new JObject {
                    { "os", "windows" },
                    { "browser", "KittyBot" },
                    { "device", "KittyBot" } }
                }
            };

            string json = JsonConvert.SerializeObject(handShake);

            Console.WriteLine(json);
            return;

            StringBuilder sb = new StringBuilder();
            sb.Append("{\"op\":2,\"d\":{\"token\":\"");
            sb.Append(Program.Token);
            sb.Append("\",\"intents\":");
            sb.Append(Intents.GUILD_MESSAGE_REACTIONS);
            sb.Append(",\"properties\":{\"os\":\"windows\",\"browser\":\"KittyBot\",\"device\":\"KittyBot\"}}}");
            Console.WriteLine(sb.ToString());

            return;

            new Bot().Start();

        }
    }
}