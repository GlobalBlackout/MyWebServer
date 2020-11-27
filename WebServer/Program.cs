using System;
using WebServer.Server;

namespace WebServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new HttpServer(1234);
            server.Start();
        }
    }
}