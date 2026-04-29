using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KittyBot.Network
{
    /*
        GET     HttpClient.GetAsync
        GET	    HttpClient.GetByteArrayAsync
        GET	    HttpClient.GetStreamAsync
        GET	    HttpClient.GetStringAsync
        POST    HttpClient.PostAsync
        PUT	    HttpClient.PutAsync
        PATCH   HttpClient.PatchAsync
        DELETE  HttpClient.DeleteAsync
        †USER   HttpClient.SendAsync
    */

    internal class Client
    {
        public HttpClient HttpClient { get; private set; }
        public static Client Instance => _instance ??= new Client();
        private static Client? _instance;

        public static string? UserAgent { get; private set; }

        public Client() {
            HttpClient = new HttpClient() {
                BaseAddress = new Uri("https://discord.com/api")
            };
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bot", Program.Token);
            HttpClient.DefaultRequestHeaders.UserAgent.ParseAdd($"DiscordBot ({Program.URL}, {Program.Version})");
        }

        public async Task GetGateway() {
            var response = await HttpClient.GetAsync("/gateway");
            JObject obj = JObject.Parse(await response.Content.ReadAsStringAsync());
            if(response.StatusCode == HttpStatusCode.OK) {
                WSS wss = new WSS((string)obj["url"]!);
                await wss.Connect();
            }
        }
    }
}
