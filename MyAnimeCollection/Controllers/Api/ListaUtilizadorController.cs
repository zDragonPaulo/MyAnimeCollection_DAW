using Microsoft.AspNetCore.Mvc;
using MyAnimeCollection.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyAnimeCollection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListaUtilizadorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ListaUtilizadorController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: api/ListaUtilizador
        [HttpGet]
        public ActionResult<IEnumerable<ListaUtilizador>> GetListaUtilizadores()
        {
            return _context.ListasUtilizador.ToList();
        }

        // GET: api/ListaUtilizador/5
        [HttpGet("{id}")]
        public ActionResult<ListaUtilizador> GetListaUtilizador(int id)
        {
            var listaUtilizador = _context.ListasUtilizador.Find(id);

            if (listaUtilizador == null)
            {
                return NotFound();
            }

            return Ok(listaUtilizador); // Retorne com Ok()
        }

        // POST: api/ListaUtilizador
        [HttpPost]
        public ActionResult<ListaUtilizador> PostListaUtilizador(ListaUtilizador listaUtilizador)
        {
            _context.ListasUtilizador.Add(listaUtilizador);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetListaUtilizador), new { id = listaUtilizador.ListaUtilizadorId }, listaUtilizador);
        }

        // PUT: api/ListaUtilizador/5
        [HttpPut("{id}")]
        public IActionResult PutListaUtilizador(int id, ListaUtilizador listaUtilizador)
        {
            if (id != listaUtilizador.ListaUtilizadorId)
            {
                return BadRequest();
            }

            _context.Entry(listaUtilizador).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/ListaUtilizador/5
        [HttpDelete("{id}")]
        public IActionResult DeleteListaUtilizador(int id)
        {
            var listaUtilizador = _context.ListasUtilizador.Find(id);
            if (listaUtilizador == null)
            {
                return NotFound();
            }

            _context.ListasUtilizador.Remove(listaUtilizador);
            _context.SaveChanges();

            return NoContent();
        }

    }
}