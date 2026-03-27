using GGC.Interfaces;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace GGC.Database
{
    public class Firebase_REST<DATA, DESERIALIZED_GET> : IRest<DATA, DESERIALIZED_GET>
    {
        public string URL { get; set; } = "https://vectorfall-e4366-default-rtdb.firebaseio.com/Scores";

        public async Task<HttpResponseMessage> AsyncCreate(DATA data)
        {
            IAPI api = new Firebase_API()
            {
                Url = $"{URL}.json ",
                Method = HttpMethod.Post,
                Body = JsonSerializer.Serialize(data)
            };

            HttpResponseMessage res = await api.SendRequest();

            return res;
        }

        public async Task<HttpResponseMessage> AsyncDelete(int index)
        {
            IAPI api = new Firebase_API()
            {
                Url = $"{URL}/{index}.json ",
                Method = HttpMethod.Delete
            };

            HttpResponseMessage res = await api.SendRequest();

            return res;
        }

        public async Task<DESERIALIZED_GET> AsyncGet()
        {
            IAPI api = new Firebase_API()
            {
                Url = $"{URL}.json",
                Method = HttpMethod.Get
            };

            HttpResponseMessage res = await api.SendRequest();

            string body = "";

            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                body = await res.Content.ReadAsStringAsync();
            }

            DESERIALIZED_GET desGet = JsonSerializer.Deserialize<DESERIALIZED_GET>(body);

            return desGet;
        }

        public async Task<HttpResponseMessage> AsyncUpdate(int index, DATA data)
        {
            IAPI api = new Firebase_API()
            {
                Url = $"{URL}/{index}.json ",
                Method = HttpMethod.Patch,
                Body = JsonSerializer.Serialize(data)
            };

            HttpResponseMessage res = await api.SendRequest();

            return res;
        }
    }
}
