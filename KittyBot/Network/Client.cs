using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http.Headers;

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
    }
}
