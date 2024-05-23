using apitreino;
using Dominio.Entidades;

namespace Dominio.Interface
{
    public interface IServicoEndereco
    {
        EnderecoDTO Adicionar(int pessoaId, EnderecoDTO enderecoDTO);
        EnderecoDTO Editar(EnderecoDTO enderecoDTO);
        IEnumerable<EnderecoDTO> Listar();
        EnderecoDTO ObterPorId(int id);
        void Remover(int id);
    }
}
