using apitreino;
using Dominio.Entidades;

namespace Dominio.Interface
{
    public interface IServicoEndereco
    {
        Endereco Adicionar(Endereco endereco);
        Endereco Editar(Endereco endereco);
        IEnumerable<Endereco> Listar();
        Endereco ObterPorId(int id);
        void Remover(int id);

    }
}
