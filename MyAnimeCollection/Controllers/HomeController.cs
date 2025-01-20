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


    public async Task<IActionResult> Index()
    {
        var animes = await _animeApiService.GetAnimesAsync(); // Obtemos os animes do serviço

        var userId = User.FindFirstValue("UserId"); // Obtém o ID do utilizador autenticado
        if (userId != null)
        {
            // Obtemos as listas do utilizador autenticado
            var userLists = await _context.UserLists
                .Where(ul => ul.UserId == int.Parse(userId))
                .ToListAsync();

            ViewBag.UserLists = userLists;
        }
        else
        {
            ViewBag.UserLists = new List<UserListModel>(); // Lista vazia caso não esteja autenticado
        }

        return View(animes);
    }



    public IActionResult Privacy()
    {
        return View();
    }
}