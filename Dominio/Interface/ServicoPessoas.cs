using apitreino;
using apitreino.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Dominio.Interface
{
    internal class ServicoPessoas
    {
    }
}
public class ServicoPessoas 
{
    private readonly APIContexto _context;

    public ServicoPessoas(APIContexto context)
    {
        _context = context;
    }

    public Pessoas Adicionar(Pessoas Pessoas)
    {
        _context.Set<Pessoas>().Add(Pessoas);
        _context.SaveChanges();
        return Pessoas;
    }

    public Pessoas Editar(Pessoas Pessoas)
    {
        _context.Entry(Pessoas).State = EntityState.Modified;
        _context.SaveChanges();
        return Pessoas;
    }
    public IEnumerable<Pessoas> Listar()
    {
        return _context.Set<Pessoas>().ToList();
    }

    public void Remover(int id)
    {
        var Pessoas = _context.Set<Pessoas>().Find(id);
        if (Pessoas != null)
        {
            _context.Set<Pessoas>().Remove(Pessoas);
            _context.SaveChanges();
        }
        else
        {
            throw new InvalidOperationException("Pessoas não encontrado para remoção.");
        }
    }

    public Pessoas ObterPorId(int id)
    {
        return _context.Set<Pessoas>().Find(id);
    }

}