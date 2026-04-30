using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace KittyBot.DiscordEvents.Guild.Messages
{
    public class Message_Reaction_Add : IDiscordEvent
    {
        public void Recieve(JToken data) {

        }
    }

    public class Message_Reaction_Remove : IDiscordEvent
    {
        public void Recieve(JToken data) {

        }
    }
}
