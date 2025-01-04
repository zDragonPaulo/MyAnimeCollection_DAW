using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimeAPI.Data;
using AnimeAPI.Models;

namespace AnimeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AnimeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all animes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAnimes()
        {
            return await _context.Animes
                .Select(a => new
                {
                    a.AnimeId,
                    a.Title,
                    a.Synopsis,
                    a.ImageURL,
                    a.NumberEpisodes,
                    Genres = a.Genres // Aqui estamos a retornar a lista de géneros como uma lista de strings
                })
                .ToListAsync();
        }

        // Get a specific anime by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetAnime(int id)
        {
            var anime = await _context.Animes
                .Where(a => a.AnimeId == id)
                .Select(a => new
                {
                    a.AnimeId,
                    a.Title,
                    a.Synopsis,
                    a.ImageURL,
                    a.NumberEpisodes,
                    Genres = a.Genres // Aqui também retornamos os géneros como uma lista de strings
                })
                .FirstOrDefaultAsync();

            if (anime == null)
                return NotFound();

            return anime;
        }

        [HttpPost]
        public async Task<ActionResult<Anime>> CreateAnime([FromBody] Anime anime)
        {
            // Não definir o AnimeId explicitamente
            _context.Animes.Add(anime);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAnime), new { id = anime.AnimeId }, anime);
        }


        // Update an existing anime
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnime(int id, [FromBody] Anime updatedAnime)
        {
            if (id != updatedAnime.AnimeId)
                return BadRequest();

            _context.Entry(updatedAnime).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnime(int id)
        {
            var anime = await _context.Animes.FindAsync(id);
            if (anime == null)
            {
                return NotFound();
            }

            _context.Animes.Remove(anime);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(new { message = "Não foi possível excluir o anime devido a dependências." });
            }

            return NoContent();
        }
    }
}
