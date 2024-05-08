using apitreino;
using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interface
{
    public class ServicoEndereco : IServicoEndereco
    {
        private readonly APIContexto _context;

        public ServicoEndereco(APIContexto context)
        {
            _context = context;
        }

        public Endereco Adicionar(int pessoaId, Endereco endereco)
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

        public Endereco Editar(int pessoaId, Endereco endereco)
        {
            var pessoa = _context.Set<Pessoas>().Find(pessoaId);
            if (pessoa != null)
            {
                pessoa.Endereco = endereco;
                _context.Entry(endereco).State = EntityState.Modified;
                _context.SaveChanges();
                return endereco;
            }
            else
            {
                throw new InvalidOperationException("Pessoa não encontrada para editar o endereço.");
            }
        }


        public IEnumerable<Endereco> Listar()
        {
            return _context.Set<Endereco>().ToList();
        }

        public Endereco ObterPorId(int pessoaId)
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

        public void Remover(int pessoaId, int enderecoId)
        {
            var pessoa = _context.Pessoass.Include(p => p.Endereco).FirstOrDefault(p => p.Id == pessoaId);

            if (pessoa != null && pessoa.Endereco != null && pessoa.Endereco.Id == enderecoId)
            {
                pessoa.Endereco = null; 

               
                if (pessoa.Endereco != null)
                {
                    _context.Enderecos.Remove(pessoa.Endereco); 
                }

                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Endereço não encontrado para remoção.");
            }
        }

    }
}
