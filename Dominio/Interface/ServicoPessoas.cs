using apitreino;
using Microsoft.EntityFrameworkCore;
using Dominio.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace Dominio.Interface
{
    public class ServicoPessoas : IServicoPessoas
    {
        private readonly APIContexto _context;

        public ServicoPessoas(APIContexto context)
        {
            _context = context;
        }

        public Pessoas Adicionar(Pessoas pessoa)
        {
            _context.Set<Pessoas>().Add(pessoa);
            _context.SaveChanges();
            return pessoa;
        }

        public Pessoas Editar(Pessoas pessoa)
        {
            _context.Entry(pessoa).State = EntityState.Modified;
            _context.SaveChanges();
            return pessoa;
        }

        public IEnumerable<Pessoas> Listar()
        {
            return _context.Set<Pessoas>().ToList();
        }

        public void Remover(int id)
        {
            var pessoa = _context.Set<Pessoas>().Find(id);
            if (pessoa != null)
            {
                _context.Set<Pessoas>().Remove(pessoa);
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Pessoa não encontrada para remoção.");
            }
        }

        public Pessoas ObterPorId(int id)
        {
            return _context.Set<Pessoas>().Find(id);
        }

        // Novos métodos para lidar com operações de endereço

        public Endereco AdicionarEndereco(int pessoaId, Endereco endereco)
        {
            var pessoa = _context.Set<Pessoas>().Find(pessoaId);
            if (pessoa != null)
            {
                pessoa.Endereco = endereco;
                _context.SaveChanges();
                return endereco;
            }
            else
            {
                throw new InvalidOperationException("Pessoa não encontrada para adicionar endereço.");
            }
        }

        public Endereco ObterEndereco(int pessoaId)
        {
            var pessoa = _context.Set<Pessoas>().Find(pessoaId);
            if (pessoa != null)
            {
                return pessoa.Endereco;
            }
            else
            {
                throw new InvalidOperationException("Pessoa não encontrada para obter endereço.");
            }
        }
    }
}
