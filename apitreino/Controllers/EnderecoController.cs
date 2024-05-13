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

        [HttpPost]
        public IActionResult Adicionar([FromBody] Endereco endereco)
        {
            try
            {
                var novoEndereco = _servicoEndereco.Adicionar(endereco);
                return CreatedAtAction(nameof(ObterPorId), new { id = novoEndereco.Id }, novoEndereco);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] Endereco endereco)
        {
            try
            {
                endereco.Id = id; // Defina o ID do endereço para corresponder ao ID fornecido na rota
                var enderecoEditado = _servicoEndereco.Editar(endereco);
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
