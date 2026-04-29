using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace KittyBot.Network
{
    public class Payload {
        public int op;
        public JToken? d;
        public int? s;
        public string? t;
    }
}
