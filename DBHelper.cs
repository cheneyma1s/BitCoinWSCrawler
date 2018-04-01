using ConsoleApp1.Pro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class DBHelper
    {

        public static string CONNSTR => System.Configuration.ConfigurationManager.ConnectionStrings["CONN"].ConnectionString;

        public static void Insert(this ProBase pro, string price, string createtime)
        {
            try
            {
                //using (var conn = new MySql.Data.MySqlClient.MySqlConnection(CONNSTR))
                //using (var cmd = new MySql.Data.MySqlClient.MySqlCommand($"INSERT INTO N(PLATFORM,TYPE,PRICE,UNIT,DEAL_TIME) VALUE('{pro.Platform}','{pro.Type}','{price}','{pro.Unit}','{createtime}')", conn))
                //{
                //    conn.Open();
                //    cmd.ExecuteNonQuery();
                //}
                Console.WriteLine($"[DB][{pro.Platform}]PRICE:{price};TIME{createtime}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DB][ERROR][{pro.Platform}]{ex.Message.Replace("\n", " ").Replace("\r", " ")}");
                try
                {
                    System.IO.File.AppendAllText($"{System.Environment.CurrentDirectory}/error{DateTime.Now.ToString("yyyyMMdd")}.log", $"[DB][ERROR][{pro.Platform}]{ex.Message.Replace("\n", " ").Replace("\r", " ")}\r\n");
                }
                catch
                {
                }
            }
        }

        public static string DateTimeToDBString(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss,ms");
        }
    }
}
