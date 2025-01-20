using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Models; // Certifique-se de ajustar o namespace conforme necessário
public class HomeController : Controller
{
    private readonly AnimeApiService _animeApiService;

    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context, AnimeApiService animeApiService)
    {
        _context = context;
        _animeApiService = animeApiService;
    }


    public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
    {
        var animes = await _animeApiService.GetAnimesAsync();

        if (startDate.HasValue && endDate.HasValue)
        {
            var filteredAnimes = await _context.UserAnimeAvaliations
                .Where(uaa => uaa.DateCreated >= startDate.Value && uaa.DateCreated <= endDate.Value)
                .GroupBy(uaa => uaa.AnimeId)
                .Select(g => new
                {
                    AnimeId = g.Key,
                    AverageRating = g.Average(uaa => uaa.Avaliation)
                })
                .OrderByDescending(a => a.AverageRating)
                .ToListAsync();

            var animeIds = filteredAnimes.Select(a => a.AnimeId).ToList();
            animes = animes.Where(a => animeIds.Contains(a.AnimeId))
                           .OrderByDescending(a => filteredAnimes.First(fa => fa.AnimeId == a.AnimeId).AverageRating)
                           .ToList();
        }

        var userId = User.FindFirstValue("UserId");
        if (userId != null)
        {
            var userLists = await _context.UserLists
                .Where(ul => ul.UserId == int.Parse(userId))
                .ToListAsync();

            ViewBag.UserLists = userLists;
        }
        else
        {
            ViewBag.UserLists = new List<UserListModel>();
        }

        return View(animes);
    }

    public IActionResult Privacy()
    {
        return View();
    }
}