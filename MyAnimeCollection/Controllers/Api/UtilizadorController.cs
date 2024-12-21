using Microsoft.AspNetCore.Mvc;
using MyAnimeCollection.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyAnimeCollection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilizadorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UtilizadorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Utilizador
        [HttpGet]
        public ActionResult<IEnumerable<Utilizador>> GetUtilizadores()
        {
            return _context.Utilizadores.ToList();
        }

        // GET: api/Utilizador/5
        [HttpGet("{id}")]
        public ActionResult<Utilizador> GetUtilizador(int id)
        {
            var utilizador = _context.Utilizadores.Find(id);

            if (utilizador == null)
            {
                return NotFound();
            }

            return utilizador;
        }

        // POST: api/Utilizador
        [HttpPost]
        public ActionResult<Utilizador> PostUtilizador(Utilizador utilizador)
        {
            _context.Utilizadores.Add(utilizador);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetUtilizador), new { id = utilizador.UtilizadorId }, utilizador);
        }

        // PUT: api/Utilizador/5
        [HttpPut("{id}")]
        public IActionResult PutUtilizador(int id, Utilizador utilizador)
        {
            if (id != utilizador.UtilizadorId)
            {
                return BadRequest();
            }

            _context.Entry(utilizador).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Utilizador/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUtilizador(int id)
        {
            var utilizador = _context.Utilizadores.Find(id);
            if (utilizador == null)
            {
                return NotFound();
            }

            _context.Utilizadores.Remove(utilizador);
            _context.SaveChanges();

            return NoContent();
        }
    }
}