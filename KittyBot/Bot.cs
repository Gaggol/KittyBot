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

        public async void Start() {
            Initialize();
            
            IsConnected = await Connect();
            
            if(IsConnected == false) {
                Console.WriteLine("Failed to Connect");
                return;
            }

            while(IsConnected) {
                Update();
            }

            Disconnect();
        }

        public void Initialize() {
            IsRunning = true;
        }

        public async Task<bool> Connect() {
            
            Console.WriteLine("Authenticating...");

            await Client.Instance.HttpClient.GetAsync("");

            Console.WriteLine("Connecting..");

            Console.WriteLine("Connected Successfully");
            return true;
        }

        public async void Disconnect() {}

        public async void Update() { }

    }
}
