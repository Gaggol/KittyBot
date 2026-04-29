using System;
using System.Collections.Generic;
using System.Text;
using KittyBot.Network;

namespace KittyBot
{
    public class Bot
    {
        public static bool IsConnected;
        public static bool IsRunning;

        public Bot() { }

        public async Task Start() {
            Client client = new Client();
            await client.GetGateway();
        }
    }
}
