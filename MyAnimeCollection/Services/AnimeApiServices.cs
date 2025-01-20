using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

public class AnimeApiService
{
    private readonly HttpClient _httpClient;

    // Constructor
    public AnimeApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Get all animes
    public async Task<List<Anime>> GetAnimesAsync()
    {
        var response = await _httpClient.GetAsync("http://localhost:5048/api/anime");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<Anime>>();
    }

    // Get an anime by its id
    public async Task<Anime> GetAnimeAsync(int id)
    {
        var response = await _httpClient.GetAsync($"http://localhost:5048/api/anime/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Anime>();
    }

    // Get animes by their ids
    public async Task<List<Anime>> GetAnimesByIdsAsync(List<int> animeIds)
    {
        var animes = new List<Anime>();

        foreach (var id in animeIds)
        {
            var anime = await GetAnimeAsync(id);
            if (anime != null)
            {
                animes.Add(anime);
            }
        }

        return animes;
    }

}

// Anime class and its properties
public class Anime
{
    public int AnimeId { get; set; }
    public string Title { get; set; }
    public string Synopsis { get; set; }
    public string? ImageURL { get; set; }
    public int NumberEpisodes { get; set; }
    public List<string> Genres { get; set; } = new List<string>();
}