using Microsoft.AspNetCore.Mvc;
using Models;
using Microsoft.EntityFrameworkCore;



public class UserListController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly AnimeApiService _animeApiService; // Serviço para interação com a API

    public UserListController(ApplicationDbContext context, AnimeApiService animeApiService)
    {
        _context = context;
        _animeApiService = animeApiService;
    }

    // 1. Adicionar um anime a uma lista
    [HttpPost]
    public async Task<IActionResult> AddAnimeToList(int animeId, int listId)
    {
        var userList = await _context.UserLists.FindAsync(listId);

        if (userList == null)
        {
            return Json(new { success = false, message = "Lista não encontrada." });
        }

        // Verificar se o anime já está na lista, utilizando o ID
        if (userList.AnimeIds.Contains(animeId))
        {
            return Json(new { success = false, message = "Anime já está na lista." });
        }

        // Adiciona o anime à lista (apenas o ID, pois AnimeIds é uma lista de IDs)
        userList.AnimeIds.Add(animeId);
        await _context.SaveChangesAsync();

        return Json(new { success = true, message = "Anime adicionado à lista!" });
    }



    // 2. Criar uma lista
    [HttpGet]
    public IActionResult CreateUserList(int? animeId)
    {
        ViewData["AnimeId"] = animeId;
        return View();
    }

    [HttpPost]
    public IActionResult CreateUserList(string name, string description, int? animeId)
    {
        try
        {
            var userId = GetAuthenticatedUserId(); // Obter o ID do utilizador autenticado.

            // Verificar se o utilizador existe na base de dados
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


    // 3. Eliminar uma lista
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

    // 4. Alterar uma lista
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

    // 5. Visualizar uma lista
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var userList = await _context.UserLists
            .Include(ul => ul.User) // Inclui o relacionamento com o usuário
            .FirstOrDefaultAsync(ul => ul.UserListId == id);

        if (userList == null)
        {
            return NotFound("Lista não encontrada.");
        }

        // Obtenha o nome do criador da lista
        ViewBag.CreatorName = userList.User?.Name ?? "Desconhecido";

        // Obtenha os animes da lista via API
        var animeIds = userList.AnimeIds;
        var animes = await _animeApiService.GetAnimesByIdsAsync(animeIds);

        ViewBag.AnimesList = animes;

        return View(userList);
    }




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

}
