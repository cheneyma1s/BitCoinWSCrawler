using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1.Pro
{
    public class Bitstamp : ProBase
    {
        public Bitstamp() : base("wss://ws.pusherapp.com/app/0ea60078504a5d9773ab?protocol=7&client=js&version=4.1.0&flash=false",
            @"{""event"":""pusher:subscribe"",""data"":{""channel"":""live_trades""}}")
        {
            this.Platform = "Bitstamp";
            this.Type = "BTC";
            this.Unit = "USD";
        }

        public override void OnMessage(object sender, WebSocket4Net.MessageReceivedEventArgs args)
        {
            try
            {
                base.OnMessage(sender, args);

                dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(args.Message);

                if (obj["event"] == "trade")
                {

                    dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(obj.data.Value);

                    string timestamp = data.timestamp;

                    var timeStr = StartDate.AddTicks(long.Parse(timestamp) * 10000000).DateTimeToDBString();

                    string price = data.price_str;

                    this.Insert(price, timeStr);
                }
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(System.Environment.CurrentDirectory + $"/{DateTime.Now.ToString("yyyyMMdd")}.log", $"[{Platform}] throw a error : {ex.Message}\r\n");
            }

        }
    }
}
