using apitreino;
using Microsoft.EntityFrameworkCore;
using Dominio.Entidades;
using System.Collections.Generic;
using System.Linq;
using prmToolkit.NotificationPattern;

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

            // Validação do nome
            if (string.IsNullOrEmpty(pessoa.Nome) || pessoa.Nome.Length < 5)
            {
                result.Errors.Add("Nome deve conter ao menos 5 caracteres.");
            }

            // Validação da idade
            if (pessoa.Idade.HasValue && pessoa.Idade < 18)
            {
                result.Errors.Add("A pessoa deve ser maior de 18 anos.");
            }

            // Validação do endereço
            if (string.IsNullOrEmpty(pessoa.Endereco?.rua) || pessoa.Endereco.rua.Length < 10)
            {
                result.Errors.Add("A rua do endereço deve ter no mínimo 10 caracteres.");
            }

            if (pessoa.Endereco?.numero <= 0)
            {
                result.Errors.Add("O número do endereço deve ser válido.");
            }

            // Verifica se o endereço já existe
            var enderecoExistente = _context.Enderecos.FirstOrDefault(e =>
                e.Cep == pessoa.Endereco.Cep &&
                e.Estado == pessoa.Endereco.Estado &&
                e.rua == pessoa.Endereco.rua &&
                e.numero == pessoa.Endereco.numero);

            if (enderecoExistente != null)
            {
                result.Errors.Add("Endereço já cadastrado.");
            }

            if (result.Errors.Count > 0)
            {
                result.Success = false;
                return result;
            }

            // Adiciona a pessoa caso todas as validações passem
            _context.Set<Pessoas>().Add(pessoa);
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
