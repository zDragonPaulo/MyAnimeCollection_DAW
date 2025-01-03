using System.Net.Http.Json;

namespace MyAnimeCollection.Services
{
    public class AnimeService
    {
        private readonly HttpClient _httpClient;

        public AnimeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<object> GetAnimeListAsync()
        {
            // Consome o endpoint da API Jikan
            var response = await _httpClient.GetFromJsonAsync<object>("https://api.jikan.moe/v4/anime");
            return response;
        }
    }
}
