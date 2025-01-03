using MyAnimeCollection.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace MyAnimeCollection.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> Index(int userId, string listName)
        {
            var lists = new AnimeLists();
            string error = null;

            try
            {
                // Simula a chamada para a API (substitua com a URL real da sua API)
                lists = await _httpClient.GetFromJsonAsync<AnimeLists>($"https://api.seuservidor.com/anime/list/{userId}");
            }
            catch
            {
                error = "Erro ao buscar listas de animes.";
            }

            // Normaliza o nome da lista
            listName = listName?.ToLower().Replace("-", " ");
            var listMapping = new Dictionary<string, List<Anime>>
            {
                { "por ver", lists.PorVer },
                { "a ver", lists.AVer },
                { "completado", lists.Completado }
            };

            // Verifica qual lista carregar
            ViewBag.ListName = listName;
            ViewBag.Error = error;
            ViewBag.List = listMapping.GetValueOrDefault(listName, null);

            return View();
        }
    }
}
