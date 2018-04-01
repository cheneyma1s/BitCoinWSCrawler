using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ConsoleApp1.Model
{
    public class QuoinexDetail
    {
        public int id { get; set; }

        public long created_at { get; set; }

        public string price { get; set; }
    }

    public class QuoinexModel
    {
        [JsonProperty("event")]
        public string Event { get; set; }

        public QuoinexDetail data { get; set; }
    }
}
