using System.Net.Http;
using System.Net.Http.Json;
using AnimeAPI.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class JinkanApiService
{
    private readonly HttpClient _httpClient;

    public JinkanApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Anime>> GetAnimesAsync()
    {
        var response = await _httpClient.GetAsync("https://api.jikan.moe/v4/anime");
        if (!response.IsSuccessStatusCode)
            throw new Exception("Failed to fetch data from JinkanAPI.");

        var data = await response.Content.ReadFromJsonAsync<JinkanApiResponse>();
        if (data == null || data.Data == null)
            throw new Exception("No data found.");

        // Log para verificar se os dados estão sendo carregados corretamente
        foreach (var item in data.Data)
        {
            Console.WriteLine($"Anime: {item.TitleEnglish ?? item.Title}");
            Console.WriteLine($"ImageURL: {item.Images?.Jpg?.ImageUrl}");
        }

        return data.Data.Select(item => new Anime
        {
            Title = item.TitleEnglish ?? item.Title,
            Synopsis = item.Synopsis,
            NumberEpisodes = item.Episodes ?? 0,
            ImageURL = item.Images?.Jpg?.ImageUrl ?? string.Empty, // Garantir que o valor é capturado
            Genres = item.Genres.Select(g => g.Name).ToList()
        }).ToList();
    }


    // Modelos para mapear a resposta da JinkanAPI
    public class JinkanApiResponse
    {
        public List<JinkanAnime> Data { get; set; }
    }

    public class JinkanAnime
    {
        public string Title { get; set; }
        public string TitleEnglish { get; set; }
        public string Synopsis { get; set; }
        public int? Episodes { get; set; }
        public List<JinkanGenre> Genres { get; set; }
        public JinkanImages Images { get; set; } // Adicione esta linha

    }
    public class JinkanImages
    {
        public JinkanImageType Jpg { get; set; }
    }
    public class JinkanImageType
    {
        [JsonPropertyName("image_url")]

        public string ImageUrl { get; set; }  // Armazena a URL da imagem como string
    }

    public class JinkanGenre
    {
        public string Name { get; set; }
    }
}
