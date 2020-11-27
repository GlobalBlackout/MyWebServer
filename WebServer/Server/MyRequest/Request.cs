namespace WebServer.Server.MyRequest
{
    public class Request
    {
        public string Method { get; set; }
        public string Url { get; set; }
        public string Host { get; set; }
        
        public Request(string method, string url, string host)
        {
            Method = method;
            Url = url;
            Host = host;
        }
    }
}