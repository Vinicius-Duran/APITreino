using apitreino;
using Dominio.Entidades;

namespace Dominio.Interface
{
    public interface IServicoPessoas
    {
        OperationResult<Pessoas> Adicionar(Pessoas Pessoas);
        Pessoas Editar(Pessoas Pessoas);
        IEnumerable<Pessoas> Listar();
        Pessoas ObterPorId(int id);
        void Remover(int id);
        OperationResult<Endereco> AdicionarEndereco(int pessoaId, Endereco endereco);
        Endereco ObterEndereco(int pessoaId);

    }
}
