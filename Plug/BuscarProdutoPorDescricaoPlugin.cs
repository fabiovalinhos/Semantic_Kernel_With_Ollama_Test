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

    [KernelFunction]
    public async Task<List<string>> BuscarPorDescricaoAsync(string palavraChave)
    {
        var produtos = await _context.Produtos
            .Where(p => EF.Functions.ILike(p.DescricaoBreve, $"%{palavraChave}%"))
            .Select(p => $"{p.Nome} - {p.DescricaoBreve}")
            .ToListAsync();

        return produtos;
    }
}

}