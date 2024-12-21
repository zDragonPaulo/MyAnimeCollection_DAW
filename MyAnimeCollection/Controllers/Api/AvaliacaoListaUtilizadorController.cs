using Microsoft.AspNetCore.Mvc;
using MyAnimeCollection.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyAnimeCollection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvaliacaoListaUtilizadorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AvaliacaoListaUtilizadorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AvaliacaoListaUtilizador
        [HttpGet]
        public ActionResult<IEnumerable<AvaliacaoListaUtilizador>> GetAvaliacoesListaUtilizador()
        {
            return _context.AvaliacoesListasUtilizador.ToList();
        }

        // GET: api/AvaliacaoListaUtilizador/5
        [HttpGet("{id}")]
        public ActionResult<AvaliacaoListaUtilizador> GetAvaliacaoListaUtilizador(int id)
        {
            var avaliacaoListaUtilizador = _context.AvaliacoesListasUtilizador.Find(id);

            if (avaliacaoListaUtilizador == null)
            {
                return NotFound();
            }

            return avaliacaoListaUtilizador;
        }

        // POST: api/AvaliacaoListaUtilizador
        [HttpPost]
        public ActionResult<AvaliacaoListaUtilizador> PostAvaliacaoListaUtilizador(AvaliacaoListaUtilizador avaliacaoListaUtilizador)
        {
            _context.AvaliacoesListasUtilizador.Add(avaliacaoListaUtilizador);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAvaliacaoListaUtilizador), new { id = avaliacaoListaUtilizador.AvaliacaoListaId }, avaliacaoListaUtilizador);
        }

        // PUT: api/AvaliacaoListaUtilizador/5
        [HttpPut("{id}")]
        public IActionResult PutAvaliacaoListaUtilizador(int id, AvaliacaoListaUtilizador avaliacaoListaUtilizador)
        {
            if (id != avaliacaoListaUtilizador.AvaliacaoListaId)
            {
                return BadRequest();
            }

            _context.Entry(avaliacaoListaUtilizador).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/AvaliacaoListaUtilizador/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAvaliacaoListaUtilizador(int id)
        {
            var avaliacaoListaUtilizador = _context.AvaliacoesListasUtilizador.Find(id);
            if (avaliacaoListaUtilizador == null)
            {
                return NotFound();
            }

            _context.AvaliacoesListasUtilizador.Remove(avaliacaoListaUtilizador);
            _context.SaveChanges();

            return NoContent();
        }
    }
}