using GGC.Interfaces;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GGC.Database
{
    public class GoogleForms_API : IAPI
    {
        private static readonly System.Net.Http.HttpClient client = new();
        public string Url { get; set; }
        public string Body { get; set; }
        public HttpMethod Method { get; set; }

        public async Task<HttpResponseMessage> SendRequest()
        {
            try
            {
                HttpRequestMessage req = new(Method, Url);

                HttpResponseMessage res = await client.SendAsync(req);
                return res;
            }
            catch (Exception e)
            {
                GD.Print(e.Message);
                return null;
            }
        }
    }
}
