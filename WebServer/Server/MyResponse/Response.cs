using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.Server.MyResponse
{
    public class Response
    {
        private HttpStatusCode status;
        private byte[] headers;
        private byte[] bodyData;
        private string mime;

        public Response(HttpStatusCode status, byte[] bodyData, string mime)
        {
            this.status = status;
            this.bodyData = bodyData;
            this.mime = mime;
            headers = Encoding.ASCII.GetBytes(GetHeaders());
        }

        public async Task SendResponseAsync(NetworkStream stream)
        {
            await stream.WriteAsync(headers, 0, headers.Length);
            await stream.WriteAsync(bodyData, 0, bodyData.Length);
        }

        private string GetHeaders()
        {
            return
                $"{HttpServer.HttpVersion} {(int) status} {status.ToString()}\n" +
                $"Date: {DateTime.Now:R}\n" +
                $"Content-Type: {mime}\n" +
                $"Content-Length: {bodyData.Length}\n" +
                "Connection: close\n" +
                $"Server: {HttpServer.ServerName}\n\n";
        }
    }
}