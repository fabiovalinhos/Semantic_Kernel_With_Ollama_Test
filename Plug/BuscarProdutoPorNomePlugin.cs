using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using Semantic_Kernel_With_Ollama_Test.Data;

namespace Semantic_Kernel_With_Ollama_Test.Plug
{
    public class BuscarProdutoPorNomePlugin
    {
    private readonly AiOllamaDbContext _context;

    public BuscarProdutoPorNomePlugin(AiOllamaDbContext context)
    {
        _context = context;
    }

    [KernelFunction("BuscarPorNome")]
    public async Task<string> BuscarPorNomeAsync(string termo)
    {
        try
        {
            var produtos = await _context.Produtos
                .Where(p => EF.Functions.ILike(p.Nome, $"%{termo}%"))
                .Select(p => $"{p.IdProduto}: {p.Nome}")
                .ToListAsync();

            if (!produtos.Any())
                return "Nenhum produto encontrado com esse nome.";

            return string.Join(Environment.NewLine, produtos);
        }
        catch (Exception ex)
        {
            // Logar exceção aqui, se desejar
            return $"Erro ao buscar produtos por nome: {ex.Message}";
        }
    }
}
}