using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KittyBot.DiscordEvents
{
    public class DiscordEventManager
    {

        public void Read(string name, JToken data) {
            Console.WriteLine($"{name} - {JsonConvert.SerializeObject(data)}");
        }

        public void CallEvent(IDiscordEvent events) {

        }
    }
}
