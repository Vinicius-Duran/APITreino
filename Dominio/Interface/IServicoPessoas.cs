using apitreino;
using Dominio.Entidades;

namespace Dominio.Interface
{
    public interface IServicoPessoas
    {
        Pessoas Adicionar(Pessoas Pessoas);
        Pessoas Editar(Pessoas Pessoas);
        IEnumerable<Pessoas> Listar();
        Pessoas ObterPorId(int id);
        void Remover(int id);
        Endereco AdicionarEndereco(int pessoaId, Endereco endereco);
        Endereco ObterEndereco(int pessoaId);

    }
}
