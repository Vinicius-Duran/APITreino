using apitreino;
using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
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

        public PessoaDTO Adicionar(PessoaDTO pessoaDTO)
        {

            var erros = new List<string>();

            if (string.IsNullOrEmpty(pessoaDTO.Nome) || pessoaDTO.Nome.Length < 5)
            {
                erros.Add("Nome deve conter ao menos 5 caracteres");
            }

            if (pessoaDTO.Idade.HasValue && pessoaDTO.Idade < 18)
            {
                erros.Add("A pessoa deve ser maior de 18 anos.");
            }

            if (erros.Any())
            {
                throw new ServiceException(string.Join("; ", erros), 400);
            }

            var pessoa = new Pessoas
            {
                Nome = pessoaDTO.Nome,
                Idade = pessoaDTO.Idade
            };

            _context.Set<Pessoas>().Add(pessoa);
            _context.SaveChanges();

            pessoaDTO.Id = pessoa.Id;
            return pessoaDTO;
        }

        public PessoaDTO Editar(PessoaDTO pessoaDTO)
        {

            var erros = new List<string>();

            if (string.IsNullOrEmpty(pessoaDTO.Nome) || pessoaDTO.Nome.Length < 5)
            {
                erros.Add("Nome deve conter ao menos 5 caracteres");
            }

            if (pessoaDTO.Idade.HasValue && pessoaDTO.Idade < 18)
            {
                erros.Add("A pessoa deve ser maior de 18 anos.");
            }

            if (erros.Any())
            {
                throw new ServiceException(string.Join("; ", erros), 400);
            }

            var pessoa = _context.Set<Pessoas>().Find(pessoaDTO.Id);
            if (pessoa != null)
            {
                pessoa.Nome = pessoaDTO.Nome;
                pessoa.Idade = pessoaDTO.Idade;

                _context.Entry(pessoa).State = EntityState.Modified;
                _context.SaveChanges();
            }

            return pessoaDTO;
        }

        public IEnumerable<PessoaDTO> Listar()
        {
            return _context.Pessoass.Include(p => p.Enderecos).Select(p => new PessoaDTO
            {
                Id = p.Id,
                Nome = p.Nome,
                Idade = p.Idade,
                Enderecos = p.Enderecos.Select(e => new EnderecoDTO
                {
                    Id = e.Id,
                    Cep = e.Cep,
                    Estado = e.Estado,
                    Rua = e.rua,
                    Numero = e.numero,
                    PessoaId = e.PessoaId
                }).ToList()
            }).ToList();
        }

        public PessoaDTO ObterPorId(int id)
        {
            var pessoa = _context.Pessoass.Include(p => p.Enderecos).FirstOrDefault(p => p.Id == id);
            if (pessoa == null)
                {
                throw new ServiceException("Pessoa não encontrada.", 404);
            }

            return new PessoaDTO
            {
                Id = pessoa.Id,
                Nome = pessoa.Nome,
                Idade = pessoa.Idade,
                Enderecos = pessoa.Enderecos.Select(e => new EnderecoDTO
                {
                    Id = e.Id,
                    Cep = e.Cep,
                    Estado = e.Estado,
                    Rua = e.rua,
                    Numero = e.numero,
                    PessoaId = e.PessoaId
                }).ToList()
            };
        }

        public void Remover(int id)
        {
            var pessoa = _context.Set<Pessoas>().Find(id);
            if (pessoa != null)
            {
                _context.Set<Pessoas>().Remove(pessoa);
                _context.SaveChanges();
            }
        }
    }
}
