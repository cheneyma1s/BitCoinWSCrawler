using ConsoleApp1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1.Pro
{
    public class ZB : ProBase
    {
        public ZB() : base("wss://api.zb.com:9999/websocket", @"{'event':'addChannel','channel':'btcusdt_trades'}")
        {
            this.Platform = "ZB";
            this.Type = "BTC";
            this.Unit = "USDT";
        }

        public override void OnMessage(object sender, WebSocket4Net.MessageReceivedEventArgs args)
        {
            try
            {
                base.OnMessage(sender, args);

                var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<ZBModel>(args.Message);

                if (obj.dataType == "trades")
                {
                    var list = obj.data.Distinct(new ZBDetail()).ToList();

                    foreach (var item in list)
                    {
                        this.Insert(item.price, item.date_string);
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
