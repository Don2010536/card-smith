using GGC.Interfaces;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GGC.Database
{
    public class Firebase_API : IAPI
    {
        private static readonly HttpClient client = new();
        public string Url { get; set; }
        public string Body { get; set; }
        public HttpMethod Method { get; set; }

        public async Task<HttpResponseMessage> SendRequest()
        {
            try
            {
                HttpRequestMessage req = new(Method, Url);

                if (Method != HttpMethod.Get && Method != HttpMethod.Delete)
                {
                    StringContent content = new(Body, Encoding.UTF8, "application/json");
                    req.Content = content;
                }

                HttpResponseMessage res = await client.SendAsync(req);
                return res;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
