namespace WebServer.Server.MyRequest
{
    public static class RequestHandler
    {
        public static Request GetRequest(string request)
        {
            if (string.IsNullOrEmpty(request))
                return null;

            var tokens = request.Replace('\n', ' ').Split(' ');
            var method = tokens[0];
            var url = tokens[1];
            var host = tokens[4];
            
            return new Request(method, url, host);
        }
    }
}