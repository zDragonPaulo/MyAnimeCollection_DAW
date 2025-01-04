/* using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

public class UserController : Controller
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    public async Task<IActionResult> Search(string query)
    {
        var users = await _userService.GetUsersAsync();
        var result = users.Where(u => u.Name.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
        return View(result);
    }
} */