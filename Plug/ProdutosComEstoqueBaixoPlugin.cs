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
    public async Task<List<string>> ListarComEstoqueBaixoAsync(int limite = 5)
    {
        var produtos = await _context.Produtos
            .Where(p => p.Quantidade <= limite)
            .Select(p => $"{p.Nome} (Qtd: {p.Quantidade})")
            .ToListAsync();

        return produtos;
    }
}
}