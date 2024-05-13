using Dominio.Entidades;

namespace apitreino
{
    public class Endereco
    {
        public int Id { get; set; }
        public int? Cep { get; set; }
        public string? Estado { get; set; }
        public string? rua { get; set; }
        public int? numero { get; set; }

        public int PessoaId { get; set; }
        public virtual Pessoas Pessoa { get; set; }
    }
}
