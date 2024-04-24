using apitreino.Controllers;
using Microsoft.EntityFrameworkCore;

namespace apitreino
{
    public class APIContexto : DbContext
    {
        public APIContexto(DbContextOptions<APIContexto> options) : base(options)
        {
        }

        public DbSet<Pessoas> Pessoass { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
