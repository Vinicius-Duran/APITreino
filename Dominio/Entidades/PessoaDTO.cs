using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class PessoaDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int? Idade { get; set; }
        public ICollection<EnderecoDTO> Enderecos { get; set; }
    }
}
