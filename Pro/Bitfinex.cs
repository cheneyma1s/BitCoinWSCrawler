using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1.Pro
{
    public class Bitfinex : ProBase
    {
        public Bitfinex() : base("wss://api.bitfinex.com/ws",
            @"{""event"":""subscribe"",""channel"":""Trades"",""pair"":""BTCUSD""}")
        {
            this.Platform = "Bitfinex";
            this.Type = "BTC";
            this.Unit = "USD";
        }

        public override void OnMessage(object sender, WebSocket4Net.MessageReceivedEventArgs args)
        {
            try
            {
                base.OnMessage(sender, args);
                object obj = null;
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject(args.Message);


                if (obj is JArray arr && arr[1].Type == JTokenType.String)
                {
                    string price = string.Empty;
                    long timestamp = 0;
                    switch (arr[1].Value<string>())
                    {
                        case "te":
                            timestamp = arr[3].Value<long>();
                            price = arr[4].Value<string>();
                            break;
                        case "tu":
                            timestamp = arr[4].Value<long>();
                            price = arr[5].Value<string>();
                            break;
                    }

                    if (!string.IsNullOrEmpty(price) && timestamp != 0)
                    {
                        var timeStr = StartDate.AddTicks(timestamp * 10000000L).DateTimeToDBString();
                        this.Insert(price, timeStr);
                    }
                }
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(System.Environment.CurrentDirectory + $"/{DateTime.Now.ToString("yyyyMMdd")}.log", $"[{Platform}] throw a error : {ex.Message}\r\n");
            }
        }
    }
}
