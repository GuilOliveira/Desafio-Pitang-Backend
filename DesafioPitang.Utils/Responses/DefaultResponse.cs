using System.Net;

namespace DesafioPitang.Utils.Responses
{
    public class DefaultResponse
    {
        public HttpStatusCode HttpStatus { get; set; }
        public List<string> Messages { get; set; }
        public object Data { get; set; }

        public DefaultResponse(HttpStatusCode status, List<string> messages)
        {
            HttpStatus = status;
            Messages = messages;
        }
    }
}
