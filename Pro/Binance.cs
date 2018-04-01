using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1.Pro
{
    public class Binance : ProBase
    {
        public Binance() : base("wss://stream2.binance.com:9443/ws/btcusdt@aggTrade.b10",
            null)
        {
            this.Platform = "Binance";
            this.Type = "BTC";
            this.Unit = "USDT";
        }

        public override void OnMessage(object sender, WebSocket4Net.MessageReceivedEventArgs args)
        {
            base.OnMessage(sender, args);

            try
            {
                dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(args.Message);

                long ticks = obj.E * 10000;

                string timeStr = StartDate.AddTicks(ticks).DateTimeToDBString();

                string price = obj.p;

                this.Insert(price, timeStr);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(System.Environment.CurrentDirectory + $"/{DateTime.Now.ToString("yyyyMMdd")}.log", $"[{Platform}] throw a error : {ex.Message}\r\n");
            }
        }
    }
}
