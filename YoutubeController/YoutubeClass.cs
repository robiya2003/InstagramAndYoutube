using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace InstagramAndYoutube.YoutubeController
{
    public static class YoutubeClass
    {
        public static async Task<string> RunApi(string url)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://ytstream-download-youtube-videos.p.rapidapi.com/dl?id={url}"),
                Headers =
    {
        { "X-RapidAPI-Key", "378914be89msh54b5cea83f654acp17487fjsne0ec013b271a" },
        { "X-RapidAPI-Host", "ytstream-download-youtube-videos.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                foreach (var item in response.Content.Headers)
                {
                    Console.WriteLine(item.Value.ToList()[0]);
                }
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return body;
            }

        }
    }
    }
