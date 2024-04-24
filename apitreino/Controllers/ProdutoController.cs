using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace apitreino.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoasController : ControllerBase
    {
        private readonly APIContexto _contexto;

        public PessoasController(APIContexto contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public IEnumerable<Pessoas> Get()
        {
            return _contexto.Pessoass.ToList();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var Pessoas = _contexto.Pessoass.FirstOrDefault(p => p.Id == id);
            if (Pessoas == null)
            {
                return NotFound();
            }
            return Ok(Pessoas);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Pessoas Pessoas)
        {
            _contexto.Pessoass.Add(Pessoas);
            _contexto.SaveChanges(); 
            return CreatedAtAction(nameof(Get), new { id = Pessoas.Id }, Pessoas);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Pessoas Pessoas)
        {
            if (id != Pessoas.Id)
            {
                return BadRequest();
            }

            _contexto.Entry(Pessoas).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _contexto.SaveChanges(); 
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var Pessoas = _contexto.Pessoass.Find(id);
            if (Pessoas == null)
            {
                return NotFound();
            }

            _contexto.Pessoass.Remove(Pessoas);
            _contexto.SaveChanges(); 
            return Ok(Pessoas);
        }
    }
}
