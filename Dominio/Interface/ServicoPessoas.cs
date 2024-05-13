using apitreino;
using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
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

        public OperationResult<Pessoas> Adicionar(Pessoas pessoa)
        {
            var result = new OperationResult<Pessoas>();

            // Validações
            if (string.IsNullOrEmpty(pessoa.Nome) || pessoa.Nome.Length < 5)
                result.Errors.Add("Nome deve conter ao menos 5 caracteres.");

            if (pessoa.Idade.HasValue && pessoa.Idade < 18)
                result.Errors.Add("A pessoa deve ser maior de 18 anos.");

            if (pessoa.Enderecos == null || !pessoa.Enderecos.Any())
                result.Errors.Add("É necessário fornecer pelo menos um endereço.");

            if (pessoa.Enderecos.Any(e =>
                string.IsNullOrEmpty(e.rua) || e.rua.Length < 10 ||
                e.numero <= 0))
            {
                result.Errors.Add("Endereço inválido.");
            }

            if (result.Errors.Any())
            {
                result.Success = false;
                return result;
            }

            // Adicionar pessoa
            _context.Pessoass.Add(pessoa);
            _context.SaveChanges();

            result.Success = true;
            result.Data = pessoa;

            return result;
        }

        public Pessoas Editar(Pessoas pessoa)
        {
            _context.Entry(pessoa).State = EntityState.Modified;
            _context.SaveChanges();
            return pessoa;
        }

        public IEnumerable<Pessoas> Listar()
        {
            return _context.Pessoass.ToList();
        }

        public void Remover(int id)
        {
            var pessoa = _context.Pessoass.Find(id);
            if (pessoa != null)
            {
                _context.Pessoass.Remove(pessoa);
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Pessoa não encontrada para remoção.");
            }
        }

        public Pessoas ObterPorId(int id)
        {
            return _context.Pessoass.Find(id);
        }

        public OperationResult<Endereco> AdicionarEndereco(int pessoaId, Endereco endereco)
        {
            var result = new OperationResult<Endereco>();

            var pessoa = _context.Pessoass.Include(p => p.Enderecos).FirstOrDefault(p => p.Id == pessoaId);
            if (pessoa != null)
            {
                if (pessoa.Enderecos == null)
                    pessoa.Enderecos = new List<Endereco>();

                pessoa.Enderecos.Add(endereco);
                _context.SaveChanges();
                result.Success = true;
                result.Data = endereco;
            }
            else
            {
                result.Success = false;
                result.Errors.Add("Pessoa não encontrada para adicionar endereço.");
            }

            return result;
        }

        public Endereco ObterEndereco(int pessoaId)
        {
            var pessoa = _context.Pessoass.Find(pessoaId);
            if (pessoa != null)
            {
                return pessoa.Enderecos.FirstOrDefault();
            }
            else
            {
                throw new InvalidOperationException("Pessoa não encontrada para obter endereço.");
            }
        }
    }

    public class OperationResult<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }

        public OperationResult()
        {
            Errors = new List<string>();
        }
    }
}
