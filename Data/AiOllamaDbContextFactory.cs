using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Semantic_Kernel_With_Ollama_Test.Data
{
    public class AiOllamaDbContextFactory : IDesignTimeDbContextFactory<AiOllamaDbContext>
    {
        public AiOllamaDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<AiOllamaDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseNpgsql(connectionString);

            return new AiOllamaDbContext(builder.Options);
        }
    }
}
