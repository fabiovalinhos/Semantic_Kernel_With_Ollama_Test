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

    [KernelFunction]
    public async Task<List<string>> BuscarPorNomeAsync(string termo)
    {
        var produtos = await _context.Produtos
            .Where(p => EF.Functions.ILike(p.Nome, $"%{termo}%"))
            .Select(p => $"{p.IdProduto}: {p.Nome}")
            .ToListAsync();

        return produtos;
    }
}
}