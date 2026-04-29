using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using KittyBot.DiscordEvents;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KittyBot.Network
{
    public class WSS
    {
        public string URL { get; private set; }

        public static WSS? Instance;
        private ClientWebSocket _webSocket;

        private TimeSpan _heartbeatInterval;
        private Stopwatch _timer;
        private bool _heartbeatAcknowledged;

        private int? _lastSequenceRecieved = null;
        private Task? _heartbeatTask;
        private Task? _recieveEventsTask;
        private DiscordEventManager _discordEventManager;

        private int _heartbeatTries = 0;
        private bool _isZombie = false;
        private bool _firstHeartbeat = false;
        private double _jitter = 0;

        private readonly CancellationTokenSource _cancelDiscordConnection;

        public WSS(string url) {
            if(string.IsNullOrEmpty(url)) {
                throw new Exception("Url Null or Empty");
            }
            URL = url;
            Instance = this;
            _webSocket = new();
            _timer = new();
            _discordEventManager = new();
            _jitter = Random.Shared.NextDouble();
            _cancelDiscordConnection = new CancellationTokenSource();
        }

        public async Task SendPayload(Payload payload, WebSocketMessageType type = WebSocketMessageType.Text) {
            string p = JsonConvert.SerializeObject(payload);

            Console.WriteLine($"Sending Payload: {p}");
            byte[] buffer = Encoding.UTF8.GetBytes(p);
            await _webSocket.SendAsync(new ArraySegment<byte>(buffer), type, true, _cancelDiscordConnection.Token);
            
        }

        private async Task Heartbeats() {
            if(_firstHeartbeat) {
                _heartbeatInterval += TimeSpan.FromSeconds(_jitter);
            }

            while(_webSocket.State == WebSocketState.Open) {
                if(_timer.Elapsed >= _heartbeatInterval) {
                    Console.WriteLine("Sending Heartbeat");
                    if(_heartbeatAcknowledged == false && _firstHeartbeat == false) {
                        _isZombie = true;
                        Console.WriteLine("Heartbeat Lost, quitting");
                        return;
                    }
                    if(_heartbeatTries > 10) {
                        Console.WriteLine("Heartbeat Timeout");
                        _isZombie = true;
                        return;
                    }

                    if(_firstHeartbeat) {
                        _heartbeatInterval -= TimeSpan.FromSeconds(_jitter);
                        _firstHeartbeat = false;
                    }

                    _heartbeatAcknowledged = false;

                    Payload heartbeat = new Payload();
                    heartbeat.op = (int)GatewayOpCodes.Heartbeat;
                    heartbeat.d = new JValue(_lastSequenceRecieved);

                    await SendPayload(heartbeat);

                    Console.WriteLine("Waiting for Heartbeat ACK");
                    await Task.Delay(1000);

                    if(_heartbeatAcknowledged) {
                        Console.WriteLine("Heartbeat ACK");
                        _timer.Restart();
                        _heartbeatTries = 0;
                    }
                    _heartbeatTries++;
                }
            }
            return;
        }

        private async Task SendHandshake() {
            Payload handShake = new Payload();
            handShake.op = (int)GatewayOpCodes.Identify;
            handShake.d = new JObject {
                { "token", Program.Token },
                { "intents", Intents.GUILD_MESSAGE_REACTIONS },
                { "properties", new JObject {
                    { "os", "windows" },
                    { "browser", "KittyBot" },
                    { "device", "KittyBot" }
                } },
            };
            await SendPayload(handShake);
        }

        private async Task CloseError(WebSocketCloseStatus status = WebSocketCloseStatus.Empty) {
            await Close(status);
        }

        public async Task Close(WebSocketCloseStatus status = WebSocketCloseStatus.NormalClosure) {
            _cancelDiscordConnection.Cancel();
            await _webSocket.CloseAsync(status, null, default);
        }

        public async Task RecieveEvents() {
            while(_webSocket.State == WebSocketState.Open) {
                var buffer = new byte[4096];

                var response = await _webSocket.ReceiveAsync(buffer, _cancelDiscordConnection.Token);

                string r = Encoding.UTF8.GetString(buffer, 0, response.Count);

                Console.WriteLine($"Recieved {r}");
                JObject json = JObject.Parse(r);

                if(json.TryGetValue("op", out var opToken)) {

                    if((int)opToken >= 4000) {
                        Console.WriteLine(opToken.ToObject<GatewayOpCodes>());
                        await CloseError();
                        continue;
                    }

                    bool hasD = json.TryGetValue("d", out var d);
                    bool hasS = json.TryGetValue("s", out var s);
                    bool hasT = json.TryGetValue("t", out var t);

                    switch(opToken.ToObject<GatewayOpCodes>()) {
                        case GatewayOpCodes.Hello: {
                            if(hasD == false) {
                                await CloseError(WebSocketCloseStatus.InvalidPayloadData);
                                continue;
                            }
                            _heartbeatInterval = TimeSpan.FromMilliseconds((double)d!["heartbeat_interval"]!);
                            _heartbeatTask = Heartbeats();
                            _timer.Restart();
                            break;
                        }
                        case GatewayOpCodes.Dispatch: {
                            if(hasS == false || hasT == false || hasD == false) {
                                await CloseError(WebSocketCloseStatus.InvalidPayloadData);
                                continue;
                            }
                            _lastSequenceRecieved = (int)s!;
                            _discordEventManager.Read((string)t!, d!);
                            break;
                        }
                        case GatewayOpCodes.HeartbeatACK: {
                            _heartbeatAcknowledged = true;
                            break;
                        }
                    }
                }
            }
        }

        public async Task Connect() {

            await _webSocket.ConnectAsync(new Uri(URL), _cancelDiscordConnection.Token);

            await SendHandshake();
            _firstHeartbeat = true;

            _recieveEventsTask = RecieveEvents();

            await Task.WhenAll(_recieveEventsTask);
        }
    }
}
