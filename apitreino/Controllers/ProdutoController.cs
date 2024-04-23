using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace apitreino.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private static List<Produto> _produtos = new List<Produto>();
        private static int _contador = 1;

        [HttpGet]
        public IEnumerable<Produto> Get()
        {
            return _produtos;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var produto = _produtos.FirstOrDefault(p => p.Id == id);
            if (produto == null)
            {
                return NotFound();
            }
            return Ok(produto);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Produto produto)
        {
            produto.Id = _contador++;
            _produtos.Add(produto);
            return CreatedAtAction(nameof(Get), new { id = produto.Id }, produto);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Produto produto)
        {
            var index = _produtos.FindIndex(p => p.Id == id);
            if (index < 0)
            {
                return NotFound();
            }
            _produtos[index] = produto;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var produto = _produtos.FirstOrDefault(p => p.Id == id);
            if (produto == null)
            {
                return NotFound();
            }
            _produtos.Remove(produto);
            return Ok(produto);
        }
    }
}
