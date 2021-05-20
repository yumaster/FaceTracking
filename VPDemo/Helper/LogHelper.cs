using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPDemo
{
    public class LogHelper
    {
        public static void Log(string contend)
        {
            string log = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DateTime.Now.ToString("yyyyMMdd") + ".log");
            File.AppendAllText(log, $"{DateTime.Now} {contend}\r\n");
        }
    }
}
