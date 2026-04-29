using System;
using System.Collections.Generic;
using System.Text;

namespace KittyBot.Network
{
    public enum GatewayOpCodes
    {
        Dispatch = 0,                   // An event was dispatched.
        Heartbeat = 1,                  // Fired periodically by the client to keep the connection alive.
        Identify = 2,                   // Starts a new session during the initial handshake.
        PresenceUpdate = 3,             // Update the client’s presence.
        VoiceStateUpdate = 4,           // Used to join/leave or move between voice channels.
        Resume = 6,                     // Resume a previous session that was disconnected.
        Reconnect = 7,                  // You should attempt to reconnect and resume immediately.
        RequestGuildMembers = 8,        // Request information about offline guild members in a large guild.
        InvalidSession = 9,             // The session has been invalidated. You should reconnect and identify/resume accordingly.
        Hello = 10,                     // Sent immediately after connecting, contains the heartbeat_interval to use.
        HeartbeatACK = 11,              // Sent in response to receiving a heartbeat to acknowledge that it has been received.
        RequestSoundboardSounds = 31,   // Request information about soundboard sounds in a set of guilds.
        RequestChannelInfo = 43,        // Request ephemeral channel data for channels in a guild.

        UnknownError = 4000,            // We’re not sure what went wrong. Try reconnecting?
        UnknownOpCode = 4001,           // You sent an invalid Gateway opcode or an invalid payload for an opcode. Don’t do that!
        DecodeError = 4002,             // You sent an invalid payload to Discord. Don’t do that!
        NotAuthenticated = 4003,        // You sent us a payload prior to identifying, or this session has been invalidated.
        AuthenticationFailed = 4004,    // The account token sent with your identify payload is incorrect.
        AlreadyAuthenticated = 4005,    // You sent more than one identify payload. Don’t do that!
        InvalidSEQ = 4007,              // The sequence sent when resuming the session was invalid. Reconnect and start a new session.
        RateLimited = 4008,             // Woah nelly! You’re sending payloads to us too quickly. Slow it down! You will be disconnected on receiving this.
        SessionTimedOut = 4009,         // Your session timed out. Reconnect and start a new one.
        InvalidShard = 4010,            // You sent us an invalid shard when identifying.
        ShardingRequired = 4011,        // The session would have handled too many guilds - you are required to shard your connection in order to connect.
        InvalidAPIVersion = 4012,       // You sent an invalid version for the gateway.
        InvalidIntents = 4013,          // You sent an invalid intent for a Gateway Intent. You may have incorrectly calculated the bitwise value.
        DisallowedIntents = 4014,       // You sent a disallowed intent for a Gateway Intent. You may have tried to specify an intent that you have not enabled or are not approved for.
    }
}
