using Microsoft.AspNetCore.Mvc;
using MyAnimeCollection.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyAnimeCollection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvaliacaoAnimeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AvaliacaoAnimeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AvaliacaoAnime
        [HttpGet]
        public ActionResult<IEnumerable<AvaliacaoAnime>> GetAvaliacoesAnime()
        {
            return _context.AvaliacoesAnime.ToList();
        }

        // GET: api/AvaliacaoAnime/5
        [HttpGet("{id}")]
        public ActionResult<AvaliacaoAnime> GetAvaliacaoAnime(int id)
        {
            var avaliacaoAnime = _context.AvaliacoesAnime.Find(id);

            if (avaliacaoAnime == null)
            {
                return NotFound();
            }

            return avaliacaoAnime;
        }

        // POST: api/AvaliacaoAnime
        [HttpPost]
        public ActionResult<AvaliacaoAnime> PostAvaliacaoAnime(AvaliacaoAnime avaliacaoAnime)
        {
            _context.AvaliacoesAnime.Add(avaliacaoAnime);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAvaliacaoAnime), new { id = avaliacaoAnime.AvaliacaoAnimeId }, avaliacaoAnime);
        }

        // PUT: api/AvaliacaoAnime/5
        [HttpPut("{id}")]
        public IActionResult PutAvaliacaoAnime(int id, AvaliacaoAnime avaliacaoAnime)
        {
            if (id != avaliacaoAnime.AvaliacaoAnimeId)
            {
                return BadRequest();
            }

            _context.Entry(avaliacaoAnime).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/AvaliacaoAnime/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAvaliacaoAnime(int id)
        {
            var avaliacaoAnime = _context.AvaliacoesAnime.Find(id);
            if (avaliacaoAnime == null)
            {
                return NotFound();
            }

            _context.AvaliacoesAnime.Remove(avaliacaoAnime);
            _context.SaveChanges();

            return NoContent();
        }
    }
}