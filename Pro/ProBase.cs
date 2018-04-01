using SuperSocket.ClientEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using WebSocket4Net;

namespace ConsoleApp1.Pro
{
    public class ProBase
    {

        public static DateTime StartDate = new DateTime(1970, 1, 1, 8, 0, 0);

        public string Platform { get; set; }

        public string Type { get; set; }

        public string Unit { get; set; }

        private string SendData;

        private string WssPath;

        public WebSocket WSSocket;

        public ProBase(string wssPath, string sendData)
        {
            WssPath = wssPath;
            SendData = sendData;
            
            WSSocket = new WebSocket(WssPath);
            
            //开启shadowsocks代理
            WSSocket.Proxy = new SuperSocket.ClientEngine.Proxy.HttpConnectProxy(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1080));
            WSSocket.MessageReceived += OnMessage;
            WSSocket.Opened += OnOpen;
            WSSocket.Error += OnError;
            WSSocket.Closed += OnClose;
            WSSocket.Open();
        }

        public virtual void OnOpen(object sender, EventArgs args)
        {
            Console.WriteLine($"[{Platform}]Connection is open.");
            if (!string.IsNullOrEmpty(this.SendData))
            {
                WSSocket.Send(SendData);
            }
        }


        public virtual void OnClose(object sender, EventArgs args)
        {
            try
            {
                pros.Add((ProBase)this.GetType().Assembly.CreateInstance(this.GetType().ToString()));
            }
            catch (Exception ex)
            {
                //System.IO.File.AppendAllText("C:\\11232.log", $"链接关闭时，创建对象出错{ex.Message}.\r\n");
            }
            Console.WriteLine($"[{Platform}]Connection is OnClose.");
            //System.IO.File.AppendAllText("C:\\11232.log", $"[{Platform}]Connection is OnClose.\r\n");


        }


        public virtual void OnError(object sender, ErrorEventArgs args)
        {
            Console.WriteLine($"[{Platform}] throw a error : {args.Exception.Message}");
            System.IO.File.AppendAllText($"{System.Environment.CurrentDirectory}/{DateTime.Now.ToString("yyyyMMdd")}.log", $"[{Platform}] throw a error : {args.Exception.Message}\r\n");
        }


        public virtual void OnMessage(object sender, MessageReceivedEventArgs args)
        {

        }

        public static List<ProBase> pros = null;

        public static void Launcher()
        {
            pros =
                new List<ProBase>()
                {
                    new OKEx(),
                    new Bitstamp(),
                    new Binance(),
                    new Bitfinex(),
                    new ZB(),
                    new Quoine(),
                    new Bitmex()
                };

        }
    }
}
