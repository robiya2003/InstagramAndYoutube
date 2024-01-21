using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace InstagramAndYoutube.YoutubeController.YoutubeMp3Controller
{
    public static class YoutubeMp3Class
    {
        public static async Task<string> RunApi(string link)
        {
            string encodedUrl = WebUtility.UrlEncode(link);
            Console.WriteLine(encodedUrl);

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://youtube-mp3-downloader2.p.rapidapi.com/ytmp3/ytmp3/?url={encodedUrl}"),
                Headers =
    {
        { "X-RapidAPI-Key", "378914be89msh54b5cea83f654acp17487fjsne0ec013b271a" },
        { "X-RapidAPI-Host", "youtube-mp3-downloader2.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
                return body;
            }
        }   }
}
