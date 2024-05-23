using Dominio.Entidades;
using Dominio.Interface;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public IActionResult Adicionar([FromBody] PessoaDTO pessoaDTO)
        {
            try
            {
                var pessoa = _servicoPessoas.Adicionar(pessoaDTO);
                return CreatedAtAction(nameof(ObterPorId), new { id = pessoa.Id }, pessoa);
            }
            catch (ServiceException ex)
            {
                Response.Headers.Add("X-Error", ex.Message);
                return StatusCode(ex.StatusCode, new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Editar([FromBody] PessoaDTO pessoaDTO)
        {
            try
            {
                var pessoa = _servicoPessoas.Editar(pessoaDTO);
                return Ok(pessoa);
            }
            catch (ServiceException ex)
            {
                Response.Headers.Add("X-Error", ex.Message);
                return StatusCode(ex.StatusCode, new { error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            try
            {
                var pessoa = _servicoPessoas.ObterPorId(id);
                return Ok(pessoa);
            }
            catch (ServiceException ex)
            {
                Response.Headers.Add("X-Error", ex.Message);
                return StatusCode(ex.StatusCode, new { error = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var pessoas = _servicoPessoas.Listar();
            return Ok(pessoas);
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            try
            {
                _servicoPessoas.Remover(id);
                return NoContent();
            }
            catch (ServiceException ex)
            {
                Response.Headers.Add("X-Error", ex.Message);
                return StatusCode(ex.StatusCode, new { error = ex.Message });
            }
        }
    }
}
