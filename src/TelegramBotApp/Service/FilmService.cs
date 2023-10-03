
using Newtonsoft.Json;
using TelegramBotApp.Farxodbek;
using TelegramBotApp.models;

namespace TelegramBotApp.Service
{
    public class FilmService
    {
        public async Task<Root> GetFilmListAsync(string name, int pageIndex = 1)
        {
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("");
                string token = "";
                var json = await client.GetStringAsync(token);
                var root = JsonConvert.DeserializeObject<Root>(json) ;
                root.Response = name ;

                return root ;
            }
        }

        public async Task<Search> GetFilmAsync(string imdbID)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("");
                string query = "";
                var json = await client.GetStringAsync(query);
                var film = JsonConvert.DeserializeObject<Search>(json);

                return film ;
            }
        }
    }
}
