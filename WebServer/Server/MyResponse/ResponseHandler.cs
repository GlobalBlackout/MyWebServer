using System.Net;
using System.Text;
using WebServer.Server.MyRequest;

namespace WebServer.Server.MyResponse
{
    public static class ResponseHandler
    {
        public static Response GetResponse(Request request)
        {
            return request == null ? NullResponse() : OkResponse();
        }

        private static Response NullResponse()
        {
            return new Response(HttpStatusCode.BadRequest, new byte[0], "text/html");
        }

        private static Response OkResponse()
        {
            return new Response(HttpStatusCode.OK, Encoding.ASCII.GetBytes("Hi"), "text/html");
        }
    }
}