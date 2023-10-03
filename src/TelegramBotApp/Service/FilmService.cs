
using Newtonsoft.Json;

namespace TelegramBotApp.Service
{
    public class FilmService
    {
        public async Task<ListOfSearch> GetFilmListAsync(string name, int pageIndex = 1)
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
                var root = JsonConvert.DeserializeObject<ListOfSearch>(json) ;
                root.SearchKey = name ;

                return root ;
            }
        }

        public async Task<Film> GetFilmAsync(string imdbID)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("");
                string query = "";
                var json = await client.GetStringAsync(query);
                var film = JsonConvert.DeserializeObject<Film>(json);

                return film ;
            }
        }
    }
}
