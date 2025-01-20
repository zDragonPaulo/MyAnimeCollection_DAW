using Microsoft.AspNetCore.Mvc;
using Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


public class UserListController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly AnimeApiService _animeApiService;

    // Constructor
    public UserListController(ApplicationDbContext context, AnimeApiService animeApiService)
    {
        _context = context;
        _animeApiService = animeApiService;
    }

    // Add anime to a list (Use Case #2)
    [HttpPost]
    public async Task<IActionResult> AddAnimeToList(int animeId, int listId)
    {
        var userList = await _context.UserLists.FindAsync(listId);

        if (userList == null)
        {
            return Json(new { success = false, message = "Lista não encontrada." });
        }

        if (userList.AnimeIds.Contains(animeId))
        {
            return Json(new { success = false, message = "Anime já está na lista." });
        }

        userList.AnimeIds.Add(animeId);
        await _context.SaveChangesAsync();

        return Json(new { success = true, message = "Anime adicionado à lista!" });
    }

    // Create a list
    [HttpGet]
    public IActionResult CreateUserList(int? animeId)
    {
        ViewData["AnimeId"] = animeId;
        return View();
    }

    //Create a list
    [HttpPost]
    public IActionResult CreateUserList(string name, string description, int? animeId)
    {
        try
        {
            var userId = GetAuthenticatedUserId();

            var userExists = _context.Users.Any(u => u.UserId == userId);
            if (!userExists)
            {
                return Unauthorized("O utilizador autenticado não existe na base de dados.");
            }

            var userList = new UserListModel
            {
                UserId = userId,
                Name = name,
                Description = description,
                AnimeIds = animeId.HasValue ? new List<int> { animeId.Value } : new List<int>()
            };

            _context.UserLists.Add(userList);
            _context.SaveChanges();

            return RedirectToAction("Details", new { id = userList.UserListId });
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro ao criar a lista: {ex.Message}");
        }
    }

    // Delete a list
    [HttpPost]
    public IActionResult DeleteList(int listId)
    {
        var userList = _context.UserLists.FirstOrDefault(ul => ul.UserListId == listId);
        if (userList == null)
        {
            return NotFound("Lista não encontrada.");
        }

        _context.UserLists.Remove(userList);
        _context.SaveChanges();

        return RedirectToAction("Index", "Home");
    }

    // Change a list
    [HttpPost]
    public IActionResult UpdateList(int listId, string name, string description, List<int> animeIds)
    {
        var userList = _context.UserLists.FirstOrDefault(ul => ul.UserListId == listId);
        if (userList == null)
        {
            return NotFound("Lista não encontrada.");
        }

        userList.Name = name;
        userList.Description = description;
        userList.AnimeIds = animeIds;

        _context.SaveChanges();

        return RedirectToAction("Details", new { id = listId });
    }

    // Details of a list
    [HttpGet("user/{id}/{id_lista}")]
    public async Task<IActionResult> Details(int id, int id_lista)
    {
        var userList = await _context.UserLists
            .Include(ul => ul.User)
            .FirstOrDefaultAsync(ul => ul.UserListId == id_lista && ul.UserId == id);

        if (userList == null)
        {
            return NotFound("Lista não encontrada.");
        }

        var userLists = await _context.UserLists.ToListAsync();
        ViewBag.UserLists = userLists;

        ViewBag.CreatorName = userList.User?.Name ?? "Desconhecido";
        ViewBag.AuthenticatedUserId = GetAuthenticatedUserId();

        var animeIds = userList.AnimeIds;
        var animes = await _animeApiService.GetAnimesByIdsAsync(animeIds);

        ViewBag.AnimesList = animes;

        var ratings = await _context.UserListAvaliations
            .Where(r => r.UserListId == id_lista)
            .ToListAsync();

        var averageRating = ratings.Any() ? ratings.Average(r => r.Avaliation / 2.0) : 0;
        ViewBag.AverageRating = averageRating;

        return View(userList);
    }

    // Rate another users list (Use Case #4)
    [HttpPost]
    public async Task<IActionResult> SubmitListRating(int listId, int stars)
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

        var rating = new UserListAvaliationModel
        {
            UserListId = listId,
            UserId = userId,
            Avaliation = stars * 2, 
            DateCreated = DateTime.UtcNow
        };

        _context.UserListAvaliations.Add(rating);
        await _context.SaveChangesAsync();

        return RedirectToAction("Details", new { id = listId });
    }

    // Get all lists of a user
    [HttpGet]
    public async Task<IActionResult> GetUserLists()
    {
        var userId = GetAuthenticatedUserId();
        var userLists = await _context.UserLists
            .Where(ul => ul.UserId == userId)
            .ToListAsync();

        return Json(userLists);
    }

    // Returns the authenticated user ID
    private int GetAuthenticatedUserId()
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
        }
        throw new Exception("O utilizador autenticado não foi encontrado.");
    }

    // Remove anime from a list
    [HttpPost]
    public async Task<IActionResult> RemoveAnimeFromList([FromBody] RemoveAnimeRequest request)
    {
        Console.WriteLine($"ID da lista recebido: {request.ListId}");
        Console.WriteLine($"ID do anime recebido: {request.AnimeId}");

        var userList = await _context.UserLists.FindAsync(request.ListId);

        if (userList == null)
        {
            return Json(new { success = false, message = "Lista não encontrada. Por favor, recarregue a página e tente novamente." });
        }

        if (!userList.AnimeIds.Contains(request.AnimeId))
        {
            return Json(new { success = false, message = "Anime não está na lista." });
        }

        userList.AnimeIds.Remove(request.AnimeId);
        await _context.SaveChangesAsync();

        return Json(new { success = true, message = "Anime removido da lista com sucesso." });
    }

    // Remove anime from a list
    public class RemoveAnimeRequest
    {
        public int AnimeId { get; set; }
        public int ListId { get; set; }
    }
}
