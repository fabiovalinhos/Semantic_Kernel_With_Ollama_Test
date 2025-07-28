
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.Ollama;
using Microsoft.SemanticKernel.Plugins.Core;
using Semantic_Kernel_With_Ollama_Test.Data;


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

                /*
                services.AddSingleton<IChatCompletionService>(_ =>
                {
                    var client = new OllamaApiClient(new Uri("http://localhost:11434"));
                    return client.AsChatCompletionService("qwen2.5-coder");
                });

                services.AddSingleton<Kernel>(sp =>
                {
                    var builder = Kernel.CreateBuilder();
                    builder.Services.AddSingleton(sp.GetRequiredService<IChatCompletionService>());
                    builder.Plugins.AddFromType<TimePlugin>();
                    return builder.Build();
                });
                */
            })
            .Build();

        //var kernel = host.Services.GetRequiredService<Kernel>();

        //var result = await kernel.InvokePromptAsync("Que horas são agora?");
        //Console.WriteLine("Resposta do modelo:");
        //Console.WriteLine(result);
    }
}
