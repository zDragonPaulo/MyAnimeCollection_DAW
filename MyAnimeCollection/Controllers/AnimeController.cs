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

    // Constructor
    public AnimeController(ApplicationDbContext context, AnimeApiService animeApiService)
    {
        _context = context;
        _animeApiService = animeApiService;
    }

    // Details of an anime
    public async Task<IActionResult> Details(int id)
    {
        var anime = await _animeApiService.GetAnimeAsync(id);
        if (anime == null)
        {
            return NotFound();
        }

        var reviews = await _context.UserAnimeAvaliations.Where(r => r.AnimeId == id).ToListAsync();
        var averageRating = reviews.Any() ? reviews.Average(r => r.Avaliation / 2.0) : 0;
        ViewBag.AverageRating = averageRating;

        return View(anime);
    }

    //Search for an anime (Use Case #1)
    public async Task<IActionResult> Search(string query)
    {
        var animes = await _animeApiService.GetAnimesAsync();

        var result = animes
            .Where(a => !string.IsNullOrEmpty(query)
                && a.Title.Contains(query, StringComparison.OrdinalIgnoreCase))
            .ToList();

        var userId = GetAuthenticatedUserId();
        if (userId != null)
        {
            ViewBag.UserLists = await _context.UserLists
                .Where(ul => ul.UserId == userId)
                .ToListAsync();
        }
        return View(result);
    }

    //Rate an anime
    [HttpPost("SubmitAvaliation")]
    public async Task<IActionResult> SubmitAvaliation(int AnimeId, int Stars)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("O utilizador não está autenticado!");
        }

        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdString))
        {
            return BadRequest("Id do utilizador não encontrado!");
        }

        if (!int.TryParse(userIdString, out int userId))
        {
            return BadRequest("Id do utilizador inválido!");
        }

        var avaliation = new UserAnimeAvaliationModel
        {
            AnimeId = AnimeId,
            UserId = userId,
            Avaliation = Stars * 2,
            DateCreated = DateTime.UtcNow
        };

        _context.UserAnimeAvaliations.Add(avaliation);
        await _context.SaveChangesAsync();

        return RedirectToAction("Details", new { id = AnimeId });
    }

    // Returns the authenticated user ID
    private int? GetAuthenticatedUserId()
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(userIdClaim, out int userId))
            {
                return userId;
            }
        }
        return null;
    }
}