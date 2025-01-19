using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;
using Models;

public class AnimeController : Controller
{
    private readonly AnimeApiService _animeApiService;
    private readonly ApplicationDbContext _context;

    public AnimeController(ApplicationDbContext context, AnimeApiService animeApiService)
    {
        _context = context;
        _animeApiService = animeApiService;
    }

    public async Task<IActionResult> Details(int id)
    {
        var anime = await _animeApiService.GetAnimeAsync(id);
        if (anime == null)
        {
            return NotFound();
        }

        // Calcular a média das avaliações
        var reviews = await _context.UserAnimeAvaliations.Where(r => r.AnimeId == id).ToListAsync();
        var averageRating = reviews.Any() ? reviews.Average(r => r.Avaliation / 2.0 + 1) : 0;
        ViewBag.AverageRating = averageRating;

        return View(anime);
    }

    public async Task<IActionResult> Search(string query)
    {
        var animes = await _animeApiService.GetAnimesAsync();
        var result = animes.Where(a => a.Title.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
        return View(result);
    }

    [HttpPost("SubmitAvaliation")]
    public async Task<IActionResult> SubmitAvaliation(int AnimeId, int Stars)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated");
        }

        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdString))
        {
            return BadRequest("User ID claim not found");
        }

        if (!int.TryParse(userIdString, out int userId))
        {
            return BadRequest("Invalid user ID");
        }

        var avaliation = new UserAnimeAvaliationModel
        {
            AnimeId = AnimeId,
            UserId = userId,
            Avaliation = Stars * 2, // Converte estrelas para a escala de 0 a 10
            DateCreated = DateTime.UtcNow
        };

        _context.UserAnimeAvaliations.Add(avaliation);
        await _context.SaveChangesAsync();

        return RedirectToAction("Details", new { id = AnimeId });
    }
}