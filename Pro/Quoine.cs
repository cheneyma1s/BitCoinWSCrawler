using ConsoleApp1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1.Pro
{
    public class Quoine : ProBase
    {
        public Quoine() : base("wss://ws.pusherapp.com/app/2ff981bb060680b5ce97?protocol=7&client=js&version=4.2.1&flash=false",
            @"{""event"":""pusher:subscribe"",""data"":{""channel"":""executions_cash_btcusd""}}")
        {
            this.Platform = "Quoine";
            this.Type = "BTC";
            this.Unit = "USD";
        }

        public override void OnMessage(object sender, WebSocket4Net.MessageReceivedEventArgs args)
        {
            try
            {
                base.OnMessage(sender, args);

                dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(args.Message);

                if (obj["event"] == "created")
                {
                    string strData = obj.data;

                    var data = Newtonsoft.Json.JsonConvert.DeserializeObject<QuoinexDetail>(strData);

                    long created_at = data.created_at;

                    string created_str = StartDate.AddTicks(created_at * 10000000).ToString("yyyy-MM-dd HH:mm:ss,ms");

                    string price = data.price;

                    this.Insert(price, created_str);
                }
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(System.Environment.CurrentDirectory + $"/{DateTime.Now.ToString("yyyyMMdd")}.log", $"[{Platform}] throw a error : {ex.Message}\r\n");
            }

        }
    }
}
