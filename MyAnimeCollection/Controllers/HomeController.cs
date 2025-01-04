using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class HomeController : Controller
{
    private readonly AnimeApiService _animeApiService;

    public HomeController(AnimeApiService animeApiService)
    {
        _animeApiService = animeApiService;
    }

    public async Task<IActionResult> Index()
    {
        var animes = await _animeApiService.GetAnimesAsync();
        return View(animes);
    }
}