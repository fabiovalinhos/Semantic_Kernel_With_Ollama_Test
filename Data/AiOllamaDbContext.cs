using Microsoft.EntityFrameworkCore;
using Semantic_Kernel_With_Ollama_Test.Domain;

namespace Semantic_Kernel_With_Ollama_Test.Data
{
    public class AiOllamaDbContext : DbContext
    {
        public AiOllamaDbContext(DbContextOptions<AiOllamaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Produtos> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aplica todas as configurações de IEntityTypeConfiguration<T> encontradas no assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AiOllamaDbContext).Assembly);
        }
    }
}
