using apitreino;
using Microsoft.EntityFrameworkCore;

namespace Dominio.Interface
{
    public class ServicoEndereco : IServicoEndereco
    {
        private readonly APIContexto _context;

        public ServicoEndereco(APIContexto context)
        {
            _context = context;
        }

        public EnderecoDTO Adicionar(int pessoaId, EnderecoDTO enderecoDTO)
        {
            var erros = new List<string>();

            var pessoa = _context.Pessoass.Find(pessoaId);

            if (pessoa == null)
            {
                erros.Add("Pessoa não encontrada.");
            }

            var enderecoExistente = _context.Enderecos.FirstOrDefault(e =>
            enderecoDTO.Cep == e.Cep &&
            enderecoDTO.Estado == e.Estado &&
            enderecoDTO.Rua == e.rua &&
            enderecoDTO.Numero == e.numero);


            if (enderecoExistente != null)
            {
                erros.Add("Endereço já cadastrado.");
            }

            if (erros.Any())
            {
                throw new ExceptionEndereco(string.Join("; ", erros), 400);
            }

            var endereco = new Endereco
            {
                Cep = enderecoDTO.Cep,
                Estado = enderecoDTO.Estado,
                rua = enderecoDTO.Rua,
                numero = enderecoDTO.Numero,
                PessoaId = pessoaId
            };

            _context.Enderecos.Add(endereco);
            _context.SaveChanges();

            enderecoDTO.Id = endereco.Id;
            return enderecoDTO;
        }

        public EnderecoDTO Editar(EnderecoDTO enderecoDTO)
        {
            var endereco = _context.Enderecos.Find(enderecoDTO.Id);
            if (endereco == null)
                throw new InvalidOperationException("Endereço não encontrado.");

            endereco.Cep = enderecoDTO.Cep;
            endereco.Estado = enderecoDTO.Estado;
            endereco.rua = enderecoDTO.Rua;
            endereco.numero = enderecoDTO.Numero;
            endereco.PessoaId = enderecoDTO.PessoaId;

            _context.Entry(endereco).State = EntityState.Modified;
            _context.SaveChanges();

            return enderecoDTO;
        }

        public IEnumerable<EnderecoDTO> Listar()
        {
            return _context.Enderecos.Select(e => new EnderecoDTO
            {
                Id = e.Id,
                Cep = e.Cep,
                Estado = e.Estado,
                Rua = e.rua,
                Numero = e.numero,
                PessoaId = e.PessoaId
            }).ToList();
        }

        public EnderecoDTO ObterPorId(int id)
        {
            var endereco = _context.Enderecos.Find(id);
            if (endereco == null)
                throw new InvalidOperationException("Endereço não encontrado.");

            return new EnderecoDTO
            {
                Id = endereco.Id,
                Cep = endereco.Cep,
                Estado = endereco.Estado,
                Rua = endereco.rua,
                Numero = endereco.numero,
                PessoaId = endereco.PessoaId
            };
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
