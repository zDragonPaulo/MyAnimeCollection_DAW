using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using BCrypt.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MyAnimeCollection.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly AnimeApiService _animeApiService;

        // Constructor
        public UserController(ApplicationDbContext context, AnimeApiService animeApiService)
        {
            _context = context;
            _animeApiService = animeApiService;
        }

        // User registration
        [HttpGet("user/register")]
        public IActionResult Register()
        {
            return View();
        }

        // User registration
        [HttpPost("user/register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Name,Password,Email,Biography,Age,ImageUrl")] UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == userModel.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "O email já está em uso.");
                    return View(userModel);
                }

                userModel.Password = BCrypt.Net.BCrypt.HashPassword(userModel.Password);
                userModel.ImageUrl = "/assets/user.png";

                _context.Add(userModel);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(userModel);
        }

        // User login
        [HttpGet("user/login")]
        public IActionResult Login()
        {
            return View();
        }

        // User login
        [HttpPost("user/login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Email e Password são obrigatórios.");
                return View();
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                ModelState.AddModelError("", "Credenciais inválidas.");
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("UserId", user.UserId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");

            await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }

        // User profile
        [HttpGet("user/{id}")]
        public async Task<IActionResult> Profile(int id)
        {
            var user = await _context.Users
                .Include(u => u.UserList)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                ViewBag.Error = "Utilizador não encontrado.";
                return View();
            }

            ViewBag.UserId = user.UserId;

            var userModel = new UserModel
            {
                ImageUrl = user.ImageUrl,
                Name = user.Name,
                Age = user.Age,
                Biography = user.Biography
            };

            var userLists = user.UserList;

            if (userLists == null || !userLists.Any())
            {
                ViewBag.Error = "Não há listas de anime para este utilizador.";
            }
            else
            {
                var userListRatings = new Dictionary<int, double>();
                foreach (var list in userLists)
                {
                    var ratings = await _context.UserListAvaliations
                        .Where(r => r.UserListId == list.UserListId)
                        .ToListAsync();
                    var averageRating = ratings.Any() ? ratings.Average(r => r.Avaliation / 2.0) : 0;
                    userListRatings[list.UserListId] = averageRating;
                }
                ViewBag.UserListRatings = userListRatings;
            }

            ViewBag.UserLists = user.UserList.ToList();
            var animeIds = userLists.SelectMany(ul => ul.AnimeIds).Distinct().ToList();
            var animes = animeIds.Any() ? await _animeApiService.GetAnimesByIdsAsync(animeIds) : new List<Anime>();
            ViewBag.AnimesList = animes;

            return View(userModel);
        }

        // Search for a user (Use Case #3)
        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return View(new List<UserModel>());
            }

            var users = await _context.Users
                .Where(u => u.Name.Contains(query) || u.Email.Contains(query))
                .ToListAsync();

            return View(users);
        }

        // Logout user
        [HttpPost("user/logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Login", "User");
        }

        // Delete user
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        // Delete user
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userModel = await _context.Users.FindAsync(id);
            if (userModel != null)
            {
                _context.Users.Remove(userModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Checks if user exists
        private bool UserModelExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
