using System.Collections.Generic;

namespace RadioFreeEuropeClient.Models
{
    internal class ResponseFromServer
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public List<IsDiff>? Result { get; set; }
    }
}
