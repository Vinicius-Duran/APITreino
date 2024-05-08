using apitreino;
using Dominio.Entidades;

namespace Dominio.Interface
{
    public interface IServicoEndereco
    {
        Endereco Adicionar(int pessoaId, Endereco endereco);
        Endereco Editar(int pessoaId, Endereco endereco);
        IEnumerable<Endereco> Listar();
        Endereco ObterPorId(int pessoaId);
        void Remover(int pessoaId, int id);

    }
}
