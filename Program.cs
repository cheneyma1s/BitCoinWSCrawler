using ConsoleApp1.Model;
using ConsoleApp1.Pro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        public static readonly DateTime StartDate = new DateTime(1970, 1, 1, 8, 0, 0);

        static void Main(string[] args)
        {
            ProBase.Launcher();

            Console.ReadLine();
        }


    }
}
