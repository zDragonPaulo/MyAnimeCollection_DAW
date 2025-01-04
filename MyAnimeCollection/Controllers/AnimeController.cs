using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

public class AnimeController : Controller
{
    private readonly AnimeApiService _animeApiService;

    public AnimeController(AnimeApiService animeApiService)
    {
        _animeApiService = animeApiService;
    }

    public async Task<IActionResult> Details(int id)
    {
        var anime = await _animeApiService.GetAnimeAsync(id);
        if (anime == null)
        {
            return NotFound();
        }
        return View(anime);
    }

    public async Task<IActionResult> Search(string query)
    {
        var animes = await _animeApiService.GetAnimesAsync();
        var result = animes.Where(a => a.Title.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
        return View(result);
    }
}