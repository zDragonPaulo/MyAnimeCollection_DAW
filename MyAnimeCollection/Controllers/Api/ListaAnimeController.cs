using Microsoft.AspNetCore.Mvc;
using MyAnimeCollection.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyAnimeCollection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListaAnimeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ListaAnimeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ListaAnime
        [HttpGet]
        public ActionResult<IEnumerable<ListaAnime>> GetListaAnimes()
        {
            return _context.ListaAnimes.ToList();
        }

        // GET: api/ListaAnime/5
        [HttpGet("{id}")]
        public ActionResult<ListaAnime> GetListaAnime(int id)
        {
            var listaAnime = _context.ListaAnimes.Find(id);

            if (listaAnime == null)
            {
                return NotFound();
            }

            return listaAnime;
        }

        // POST: api/ListaAnime
        [HttpPost]
        public ActionResult<ListaAnime> PostListaAnime(ListaAnime listaAnime)
        {
            _context.ListaAnimes.Add(listaAnime);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetListaAnime), new { id = listaAnime.ListaAnimeId }, listaAnime);
        }

        // PUT: api/ListaAnime/5
        [HttpPut("{id}")]
        public IActionResult PutListaAnime(int id, ListaAnime listaAnime)
        {
            if (id != listaAnime.ListaAnimeId)
            {
                return BadRequest();
            }

            _context.Entry(listaAnime).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/ListaAnime/5
        [HttpDelete("{id}")]
        public IActionResult DeleteListaAnime(int id)
        {
            var listaAnime = _context.ListaAnimes.Find(id);
            if (listaAnime == null)
            {
                return NotFound();
            }

            _context.ListaAnimes.Remove(listaAnime);
            _context.SaveChanges();

            return NoContent();
        }
    }
}