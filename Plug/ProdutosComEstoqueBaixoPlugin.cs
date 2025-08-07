using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using Semantic_Kernel_With_Ollama_Test.Data;

namespace Semantic_Kernel_With_Ollama_Test.Plug
{
    public class ProdutosComEstoqueBaixoPlugin
    {
        private readonly AiOllamaDbContext _context;

        public ProdutosComEstoqueBaixoPlugin(AiOllamaDbContext context)
        {
            _context = context;
        }

        [KernelFunction]
        public async Task<string> ListarComEstoqueBaixoAsync(int limite = 5)
        {
            try
            {
                var produtos = await _context.Produtos
                    .Where(p => p.Quantidade <= limite)
                    .Select(p => $"{p.Nome} (Qtd: {p.Quantidade})")
                    .ToListAsync();

                if (!produtos.Any())
                    return "Nenhum produto com estoque baixo encontrado.";

                return string.Join(Environment.NewLine, produtos);
            }
            catch (Exception ex)
            {
                // Logar exceção aqui, se desejar
                return $"Erro ao listar produtos com estoque baixo: {ex.Message}";
            }
        }
    }
}