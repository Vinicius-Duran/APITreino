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

        [HttpPost("{pessoaId}/Adicionar")]
        public IActionResult AdicionarEndereco(int pessoaId, [FromBody] EnderecoDTO enderecoDTO)
        {
            try
            {
                var novoEndereco = _servicoEndereco.Adicionar(pessoaId, enderecoDTO);
                return CreatedAtAction(nameof(ObterPorId), new { id = novoEndereco.Id }, novoEndereco);
            }
            catch (ExceptionEndereco ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] EnderecoDTO enderecoDTO)
        {
            if (id != enderecoDTO.Id)
                return BadRequest(new { error = "ID mismatch" });

            try
            {
                var enderecoEditado = _servicoEndereco.Editar(enderecoDTO);
                return Ok(enderecoEditado);
            }
            catch (ExceptionEndereco ex)
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

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            try
            {
                var endereco = _servicoEndereco.ObterPorId(id);
                return Ok(endereco);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            try
            {
                _servicoEndereco.Remover(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
