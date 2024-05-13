using apitreino;
using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominio.Interface
{
    public class ServicoEndereco : IServicoEndereco
    {
        private readonly APIContexto _context;

        public ServicoEndereco(APIContexto context)
        {
            _context = context;
        }

        public Endereco Adicionar(Endereco endereco)
        {
            _context.Enderecos.Add(endereco);
            _context.SaveChanges();
            return endereco;
        }

        public Endereco Editar(Endereco endereco)
        {
            _context.Entry(endereco).State = EntityState.Modified;
            _context.SaveChanges();
            return endereco;
        }

        public IEnumerable<Endereco> Listar()
        {
            return _context.Enderecos.ToList();
        }

        public Endereco ObterPorId(int id)
        {
            return _context.Enderecos.Find(id);
        }

        public void Remover(int id)
        {
            var endereco = _context.Enderecos.Find(id);
            if (endereco != null)
            {
                _context.Enderecos.Remove(endereco);
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Endereço não encontrado para remoção.");
            }
        }
    }
}
