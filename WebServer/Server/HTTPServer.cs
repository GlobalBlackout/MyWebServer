using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using WebServer.Server.MyRequest;
using WebServer.Server.MyResponse;

namespace WebServer.Server
{
    public class HttpServer
    {
        public const string HttpVersion = "HTTP/1.1"; 
        public const string ServerName = "MyWebServer"; 
        
        private bool _running;
        private readonly TcpListener _listener;
        private readonly int _port;
        
        public HttpServer(int port)
        {
            _port = port;
            _listener = new TcpListener(IPAddress.Any, _port);
        }

        public void Start()
        {
            var serverThread = new Thread(RunAsync().Wait);
            serverThread.Start();
        }

        private async Task RunAsync()
        {
            _running = true;
            _listener.Start();
            Console.WriteLine($"The server is started end is listening on port {_port}");

            while (_running)
            {
                Console.WriteLine("Waiting for connection...");
                var client = await _listener.AcceptTcpClientAsync();
                Console.WriteLine("A client has connected");
                await HandleClientAsync(client);
                
                client.Close();
            }
            
            _listener.Stop();
            Console.WriteLine("The server is close");
        }

        private static async Task HandleClientAsync(TcpClient client)
        {
            var reader = new StreamReader(client.GetStream());
            var msg = "";
            while (reader.Peek() != -1)
            {
                msg += await reader.ReadLineAsync() + '\n';
            }
            
            var request = RequestHandler.GetRequest(msg);
            var response = ResponseHandler.GetResponse(request);
            await response.SendResponseAsync(client.GetStream());
        }
    }
}