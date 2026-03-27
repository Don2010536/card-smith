using System.Net.Http;
using System.Threading.Tasks;

namespace GGC.Interfaces
{
    public interface IAPI
    {
        public string Url { get; set; }
        public string Body { get; set; }
        public HttpMethod Method { get; set; }

        public Task<HttpResponseMessage> SendRequest();
    }
}
