using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1.Pro
{
    public class Bitmex : ProBase
    {
        public Bitmex() : base("wss://www.bitmex.com/realtime?subscribe=trade:XBTUSD",
            @"")
        {
            this.Platform = "Bitmex";
            this.Type = "BTC";
            this.Unit = "USD";
        }

        public override void OnMessage(object sender, WebSocket4Net.MessageReceivedEventArgs args)
        {
            try
            {
                base.OnMessage(sender, args);

                dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(args.Message);

                if (obj["table"] == "trade")
                {

                    foreach (dynamic item in obj.data)
                    {
                        DateTime dt = item.timestamp;
                        dt = dt.AddHours(8);

                        string price = item.price;

                        this.Insert(price, dt.DateTimeToDBString());
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
