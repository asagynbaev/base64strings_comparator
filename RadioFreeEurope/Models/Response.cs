using System.Collections.Generic;

namespace RadioFreeEurope.Models
{
    public class Response
    {
        public Response()
        {
        }

        public Response(int code, string message)
        {
            (Code, Message) = (code, message);
        }

        public Response(int code, string message, List<IsDiff> result)
        {
            (Code, Message, Result) = (code, message, result);
        }

        public int Code { get; set; }

        public string Message { get; set; }

        public List<IsDiff>? Result { get; set; }
    }
}
