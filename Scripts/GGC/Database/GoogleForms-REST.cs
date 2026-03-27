using GGC.Arcade;
using GGC.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GGC.Database
{
    public class GoogleForms_REST
    {
        public const string URL_POST = "https://docs.google.com/forms/d/e/1FAIpQLSf2IxRvA-CiPxCD_VVxtXRn37ye9mzdoxlMFO1Cpp-OCmXumA/formResponse";
        public const string URL_GET = "https://opensheet.elk.sh/1nFBQDx0CE2WHV_IxY6vA4y04mjNYo11lsthMHyfm05g/Data";

        public async Task<HttpResponseMessage> AsyncCreate(Arcade.HighScoreEntry data)
        {
            IAPI api = new GoogleForms_API()
            {
                Url = $"{URL_POST}?submit=Submit&usp=pp_url&entry.1591380162={data.Initials}&entry.2045216915={data.Score}",
                Method = HttpMethod.Post,
            };

            HttpResponseMessage res = await api.SendRequest();

            return res;
        }

        public async Task<HttpResponseMessage> AsyncDelete(int index)
        {
            IAPI api = new GoogleForms_API()
            {
                Url = URL_GET,
                Method = HttpMethod.Get,
            };

            HttpResponseMessage res = await api.SendRequest();

            return res;
        }

        public async Task<Arcade.HighScoreEntry[]> AsyncGet()
        {
            IAPI api = new GoogleForms_API()
            {
                Url = URL_GET,
                Method = HttpMethod.Get
            };

            HttpResponseMessage res = await api.SendRequest();

            string body = "";

            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                body = await res.Content.ReadAsStringAsync();
            }

            if (body != "")
            {
                GFScoreEntry[] desGet = JsonSerializer.Deserialize<GFScoreEntry[]>(body);
                List<Arcade.HighScoreEntry> entries = [];
                int rank = 1;

                foreach (GFScoreEntry des in desGet)
                {
                    entries.Add(new Arcade.HighScoreEntry()
                    {
                        Rank = rank,
                        Initials = des.Initials,
                        Score = int.Parse(des.Score)
                    });

                    rank++;

                    if (rank == 11)
                    {
                        break;
                    }
                }

                return entries.ToArray();
            } 
            else
            {
                return [];
            }
        }

        public Task<HttpResponseMessage> AsyncUpdate(int index, int data)
        {
            throw new System.NotImplementedException();
        }
    }
}
