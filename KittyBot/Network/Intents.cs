using System;
using System.Collections.Generic;
using System.Text;

namespace KittyBot.Network
{
    public static class Intents
    {
        public const uint GUILDS = 1 << 0;
        public const uint GUILD_MEMBERS = 1 << 1;
        public const uint GUILD_MODERATION = 1 << 2;
        public const uint GUILD_EXPRESSIONS = 1 << 3;
        public const uint GUILD_INTEGRATIONS = 1 << 4;
        public const uint GUILD_WEBHOOKS = 1 << 5;
        public const uint GUILD_INVITES = 1 << 6;
        public const uint GUILD_VOICE_STATES = 1 << 7;
        public const uint GUILD_PRESENCES = 1 << 8;
        public const uint GUILD_MESSAGES = 1 << 9;
        public const uint GUILD_MESSAGE_REACTIONS = 1 << 10;
        public const uint GUILD_MESSAGE_TYPING = 1 << 11;
        public const uint DIRECT_MESSAGES = 1 << 12;
        public const uint DIRECT_MESSAGE_REACTIONS = 1 << 13;
        public const uint DIRECT_MESSAGE_TYPING = 1 << 14;
        public const uint MESSAGE_CONTENT = 1 << 15;
        public const uint GUILD_SCHEDULED_EVENTS = 1 << 16;
        public const uint AUTO_MODERATION_CONFIGURATION = 1 << 20;
        public const uint AUTO_MODERATION_EXECUTION = 1 << 21;
        public const uint GUILD_MESSAGE_POLLS = 1 << 24;
        public const uint DIRECT_MESSAGE_POLLS = 1 << 25;
    }

}