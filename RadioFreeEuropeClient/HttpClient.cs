using Newtonsoft.Json;
using RadioFreeEuropeClient.Models;
using System.IO;
using System.Net;
using System.Text;

namespace RadioFreeEuropeClient
{
    internal class HttpClient
    {
        public ResponseFromServer SendRequest(Diff diff)
        {
            string url;

            if (diff.Type == DiffType.Left) url = $"https://localhost:44381/v1/diff/{diff.Id}/left";
            else if (diff.Type == DiffType.Right) url = $"https://localhost:44381/v1/diff/{diff.Id}/right";
            else url = $"https://localhost:44381/v1/diff/{diff.Id}";

            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";

            string postData = System.Text.Json.JsonSerializer.Serialize(diff.Base64Value);
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response = request.GetResponse();

            using (dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string readToEnd = reader.ReadToEnd();
                ResponseFromServer res = JsonConvert.DeserializeObject<ResponseFromServer>(readToEnd);
                return res;
            }
        }
    }
}
