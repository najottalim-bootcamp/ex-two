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
                client.BaseAddress = new Uri("https://www.omdbapi.com/");
                string query = $"?apikey=fab15ecd&s={name}&page={pageIndex}";
                var json = await client.GetStringAsync(query);
                var root = JsonConvert.DeserializeObject<Root>(json) ;
                root.SearchKey = name;
                root.pageIndex = pageIndex;
                return root ;
            }
        }

        public async Task<Search> GetFilmAsync(string imdbID)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://www.omdbapi.com/");
                string query = $"?apikey=fab15ecd&i={imdbID}";
                var json = await client.GetStringAsync(query);
                var film = JsonConvert.DeserializeObject<Search>(json);

                return film;
            }
        }
    }
}
