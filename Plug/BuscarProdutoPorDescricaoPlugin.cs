using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using Semantic_Kernel_With_Ollama_Test.Data;

namespace Semantic_Kernel_With_Ollama_Test.Plug
{
    public class BuscarProdutoPorDescricaoPlugin
{
    private readonly AiOllamaDbContext _context;

    public BuscarProdutoPorDescricaoPlugin(AiOllamaDbContext context)
    {
        _context = context;
    }

    [KernelFunction("BuscarPorDescricao")]
    public async Task<string> BuscarPorDescricaoAsync(string palavraChave)
    {
        try
        {
            var produtos = await _context.Produtos
                .Where(p => EF.Functions.ILike(p.DescricaoBreve, $"%{palavraChave}%"))
                .Select(p => $"{p.Nome} - {p.DescricaoBreve}")
                .ToListAsync();

            if (!produtos.Any())
                return "Nenhum produto encontrado com essa descrição.";

            return string.Join(Environment.NewLine, produtos);
        }
        catch (Exception ex)
        {
            // Logar exceção aqui, se desejar
            return $"Erro ao buscar produtos: {ex.Message}";
        }
    }
}


}