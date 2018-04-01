using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1.Pro
{
    public class OKEx : ProBase
    {
        public OKEx() : base("wss://real.okex.com:10440/websocket", @"{'event':'addChannel','channel':'ok_sub_spot_btc_usdt_ticker'}")
        {
            this.Platform = "OKEX";
            this.Type = "BTC";
            this.Unit = "USDT";
        }

        public override void OnMessage(object sender, WebSocket4Net.MessageReceivedEventArgs args)
        {
            try
            {
                base.OnMessage(sender, args);

                dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(args.Message);

                foreach (dynamic item in obj)
                {

                    if (item.channel == "ok_sub_spot_btc_usdt_ticker")
                    {
                        long timestamp = item.data.timestamp * 10000;

                        var timeStr = StartDate.AddTicks(timestamp).DateTimeToDBString();

                        string price = item.data.buy;

                        this.Insert(price, timeStr);
                    }
                }
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(System.Environment.CurrentDirectory + $"/{DateTime.Now.ToString("yyyyMMdd")}.log", $"[{Platform}] throw a error : {ex.Message}\r\n");
            }

        }

        public override void OnClose(object sender, EventArgs args)
        {
            base.OnClose(sender, args);

            //ProBase.pros.Add(new OKEx());
        }

    }
}
