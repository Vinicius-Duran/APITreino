using Microsoft.AspNetCore.Mvc;
using Dominio.Interface;
using Dominio.Entidades;

namespace apitreino.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoasController : ControllerBase
    {
        private readonly IServicoPessoas _servicoPessoas;

        public PessoasController(IServicoPessoas servicoPessoas)
        {
            _servicoPessoas = servicoPessoas;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var pessoas = _servicoPessoas.Listar();
            return Ok(pessoas);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var pessoa = _servicoPessoas.ObterPorId(id);
            if (pessoa == null)
            {
                return NotFound();
            }
            return Ok(pessoa);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Pessoas pessoa)
        {
            var novaPessoa = _servicoPessoas.Adicionar(pessoa);
            return CreatedAtAction(nameof(Get), new { id = novaPessoa.Id }, novaPessoa);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Pessoas pessoa)
        {
            if (id != pessoa.Id)
            {
                return BadRequest();
            }

            var pessoaExistente = _servicoPessoas.ObterPorId(id);
            if (pessoaExistente == null)
            {
                return NotFound();
            }

            var pessoaAtualizada = _servicoPessoas.Editar(pessoa);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pessoa = _servicoPessoas.ObterPorId(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            _servicoPessoas.Remover(id);
            return Ok(pessoa);
        }

        // Novos métodos para manipular operações de endereço

        [HttpPost("{pessoaId}/endereco")]
        public IActionResult AdicionarEndereco(int pessoaId, [FromBody] Endereco endereco)
        {
            try
            {
                var enderecoAdicionado = _servicoPessoas.AdicionarEndereco(pessoaId, endereco);
                return CreatedAtAction(nameof(GetEndereco), new { pessoaId }, enderecoAdicionado);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{pessoaId}/endereco")]
        public IActionResult GetEndereco(int pessoaId)
        {
            try
            {
                var endereco = _servicoPessoas.ObterEndereco(pessoaId);
                return Ok(endereco);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
