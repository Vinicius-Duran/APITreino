using apitreino;
using Dominio.Entidades;

namespace Dominio.Interface
{
    public interface IServicoPessoas
    {
        PessoaDTO Adicionar(PessoaDTO pessoaDTO);
        PessoaDTO Editar(PessoaDTO pessoaDTO);
        IEnumerable<PessoaDTO> Listar();
        PessoaDTO ObterPorId(int id);
        void Remover(int id);
    }
}
