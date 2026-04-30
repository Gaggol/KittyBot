using System;
using System.Collections.Generic;
using System.Text;
using KittyBot.DiscordEvents.Guild.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KittyBot.DiscordEvents
{
    public static class DiscordEventManager
    {
        public static Dictionary<string, IDiscordEvent> _discordEvents;

        static DiscordEventManager() {
            _discordEvents = new Dictionary<string, IDiscordEvent>() {
                { "MESSAGE_CREATE", new Message_Create() },
                { "MESSAGE_REACTION_ADD", new Message_Reaction_Add() },
                { "MESSAGE_REACTION_REMOVE", new Message_Reaction_Remove() },
                { "READY", new Ready() },
            };
        }

        public static void Read(string name, JToken data) {
            if(_discordEvents.TryGetValue(name, out var _dEvent)) {
                _dEvent.Recieve(data);
            } else {
                Console.WriteLine($"{name} - {JsonConvert.SerializeObject(data)}");
            }
        }
    }
}
