using MyAnimeCollection.Services;
using Microsoft.AspNetCore.Mvc;

namespace MyAnimeCollection.Controllers.Web
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimeController : ControllerBase
    {
        private readonly AnimeService _animeService;

        public AnimeController(AnimeService animeService)
        {
            _animeService = animeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAnimeList()
        {
            var animeList = await _animeService.GetAnimeListAsync();
            return Ok(animeList);
        }
    }
}
