using apitreino;

namespace Dominio.Entidades
{
    public class Pessoas
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public int? Idade { get; set; }

        public int EnderecoId { get; set; }
        public virtual Endereco Endereco { get; set; }

    }

}
