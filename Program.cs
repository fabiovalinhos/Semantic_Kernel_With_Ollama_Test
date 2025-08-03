using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.SemanticKernel;
using Semantic_Kernel_With_Ollama_Test.Data;
using Semantic_Kernel_With_Ollama_Test.Plug;


class Program
{
    static async Task Main(string[] args)
    {
        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddDbContext<AiOllamaDbContext>(options =>
                    options.UseNpgsql(hostContext.Configuration.GetConnectionString("DefaultConnection")));

                services.AddSingleton(_ => new HttpClient { Timeout = TimeSpan.FromMinutes(5) });

                services.AddScoped<Kernel>(sp =>
                {
                    var builder = Kernel.CreateBuilder();
                    builder.AddOllamaChatCompletion(
                        modelId: "qwen2.5-coder",
                        endpoint: new Uri("http://localhost:11434")
                    );

                    var dbContext = sp.GetRequiredService<AiOllamaDbContext>();

                    builder.Plugins.Add(KernelPluginFactory.CreateFromType<HoraAtualPlugin>());

                    builder.Plugins.Add(KernelPluginFactory.CreateFromObject(new BuscarProdutoPorDescricaoPlugin(dbContext)));
                    builder.Plugins.Add(KernelPluginFactory.CreateFromObject(new ProdutosComEstoqueBaixoPlugin(dbContext)));
                    builder.Plugins.Add(KernelPluginFactory.CreateFromObject(new BuscarProdutoPorNomePlugin(dbContext)));

                    return builder.Build();
                });
            })
            .Build();

        var kernel = host.Services.GetRequiredService<Kernel>();

        // Obtém a função do plugin
        var result = await kernel.InvokeAsync<string>("HoraAtualPlugin", "GetCurrentTime");


        Console.WriteLine("Resposta do modelo:");
        Console.WriteLine(result);
    }
}
