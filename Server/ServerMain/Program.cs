using ServerFunctionality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncServer server = new AsyncServer(IPAddress.Parse("127.0.0.1"),2137);
            server.Start();
        }
    }
}
