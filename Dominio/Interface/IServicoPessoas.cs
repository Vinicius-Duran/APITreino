using apitreino.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interface
{
    public interface IServicoPessoas
    {
        Pessoas Adicionar(Pessoas Pessoas);
        Pessoas Editar(Pessoas Pessoas);
        IEnumerable<Pessoas> Listar();
        Pessoas ObterPorId(int id);
        void Remover(int id);
    }
}
