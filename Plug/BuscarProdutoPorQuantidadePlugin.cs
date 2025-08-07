using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using Semantic_Kernel_With_Ollama_Test.Data;
using System.ComponentModel;

namespace Semantic_Kernel_With_Ollama_Test.Plug
{
    public class BuscarProdutoPorQuantidadePlugin
    {
        private readonly AiOllamaDbContext _context;

        public BuscarProdutoPorQuantidadePlugin(AiOllamaDbContext context)
        {
            _context = context;
        }

        [KernelFunction("BuscarPorQuantidade")]
        [Description("Busca produtos com base em uma quantidade mínima ou máxima.")]
        public async Task<string> BuscarPorQuantidadeAsync(
            [Description("A quantidade mínima de produtos a ser considerada. Opcional.")] int? quantidadeMinima = null,
            [Description("A quantidade máxima de produtos a ser considerada. Opcional.")] int? quantidadeMaxima = null)
        {
            try
            {
                if (!quantidadeMinima.HasValue && !quantidadeMaxima.HasValue)
                {
                    return "É necessário fornecer uma quantidade mínima ou máxima para a busca.";
                }

                var query = _context.Produtos.AsQueryable();

                if (quantidadeMinima.HasValue)
                {
                    query = query.Where(p => p.Quantidade >= quantidadeMinima.Value);
                }

                if (quantidadeMaxima.HasValue)
                {
                    query = query.Where(p => p.Quantidade <= quantidadeMaxima.Value);
                }

                var produtos = await query
                    .Select(p => $"{p.Nome} (Qtd: {p.Quantidade})")
                    .ToListAsync();

                if (!produtos.Any())
                {
                    return "Nenhum produto encontrado com os critérios de quantidade especificados.";
                }

                return string.Join(Environment.NewLine, produtos);
            }
            catch (Exception ex)
            {
                return $"Erro ao buscar produtos por quantidade: {ex.Message}";
            }
        }
    }
}