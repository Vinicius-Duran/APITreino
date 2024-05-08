using Dominio.Interface;
using Microsoft.AspNetCore.Mvc;

namespace apitreino.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnderecosController : ControllerBase
    {
        private readonly IServicoEndereco _servicoEndereco;

        public EnderecosController(IServicoEndereco servicoEndereco)
        {
            _servicoEndereco = servicoEndereco;
        }

        [HttpPost("{pessoaId}")]
        public IActionResult Adicionar(int pessoaId, [FromBody] Endereco endereco)
        {
            try
            {
                var novoEndereco = _servicoEndereco.Adicionar(pessoaId, endereco);
                return CreatedAtAction(nameof(ObterPorId), new { pessoaId = pessoaId }, novoEndereco);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{pessoaId}")]
        public IActionResult Editar(int pessoaId, [FromBody] Endereco endereco)
        {
            try
            {
                var enderecoEditado = _servicoEndereco.Editar(pessoaId, endereco);
                return Ok(enderecoEditado);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var enderecos = _servicoEndereco.Listar();
            return Ok(enderecos);
        }

        [HttpGet("{pessoaId}")]
        public IActionResult ObterPorId(int pessoaId)
        {
            try
            {
                var endereco = _servicoEndereco.ObterPorId(pessoaId);
                return Ok(endereco);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpDelete("{pessoaId}/{Id}")]
        public IActionResult Remover(int pessoaId, int Id)
        {
            try
            {
                _servicoEndereco.Remover(pessoaId, Id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
