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
                        modelId: "llama3.1:8b",
                        endpoint: new Uri("http://localhost:11434")
                    );

                    var dbContext = sp.GetRequiredService<AiOllamaDbContext>();

                    builder.Plugins.Add(KernelPluginFactory.CreateFromType<HoraAtualPlugin>());

                    builder.Plugins.Add(KernelPluginFactory.CreateFromObject(new BuscarProdutoPorDescricaoPlugin(dbContext)));
                    builder.Plugins.Add(KernelPluginFactory.CreateFromObject(new ProdutosComEstoqueBaixoPlugin(dbContext)));
                    builder.Plugins.Add(KernelPluginFactory.CreateFromObject(new BuscarProdutoPorNomePlugin(dbContext)));
                    builder.Plugins.Add(KernelPluginFactory.CreateFromObject(new BuscarProdutoPorQuantidadePlugin(dbContext)));

                    return builder.Build();
                });
            })
            .Build();

        var kernel = host.Services.GetRequiredService<Kernel>();

        // Prompt inicial para orientar o modelo
        var promptInicial = """
                Você é um assistente conectado a um sistema de produtos com acesso aos seguintes plugins:

                1. `BuscarProdutoPorDescricaoPlugin.BuscarPorDescricao`: Encontra produtos que contenham determinada palavra-chave em suas descrições.
                2. `BuscarProdutoPorNomePlugin.BuscarPorNome`: Encontra produtos que contenham determinada palavra-chave no nome.
                3. `BuscarProdutoPorQuantidadePlugin.BuscarPorQuantidade`: Filtra produtos com base em uma quantidade mínima, máxima ou ambas.
                4. `ProdutosComEstoqueBaixoPlugin.ListarComEstoqueBaixo`: Lista os produtos com estoque abaixo de um limite padrão (5 unidades por padrão).
                5. `HoraAtualPlugin.GetCurrentTime`: Informa a hora atual.

                Interprete as perguntas do usuário mesmo que ele não use os termos exatos dos métodos acima. Considere sinônimos e variações na linguagem natural.

                Regras importantes para entender e agir:
                - Se o usuário perguntar sobre **quantidade**, **estoque baixo**, **acima de X unidades** ou **abaixo de X unidades**, use o plugin de quantidade ou estoque baixo.
                - Se o usuário falar **nome** ou mencionar parte do nome do produto, use o plugin de nome.
                - Se o usuário mencionar **descrição**, ou palavras relacionadas ao conteúdo do produto, use o plugin de descrição.
                - Se o usuário mencionar **hora**, **que horas são**, **horário atual**, ou qualquer variação disso, use o plugin de hora.
                - Sempre que possível, extraia números (como quantidades) da pergunta e aplique nos parâmetros corretos dos plugins.
                - Responda apenas com os resultados vindos dos plugins, sem dizer que você "não tem acesso ao banco", pois você tem.

                Comece respondendo a partir das próximas perguntas do usuário com base nas regras acima.
                """;

        Console.WriteLine("Inicializando o assistente...");
        await kernel.InvokePromptAsync<string>(promptInicial);

        Console.WriteLine("Pronto! Digite sua pergunta sobre produtos ou 'que horas são?'.");
        Console.WriteLine("Pressione Enter sem digitar nada para sair.");

        while (true)
        {
            Console.Write("Sua pergunta: ");
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Saindo do assistente.");
                break;
            }

            try
            {
                var result = await kernel.InvokePromptAsync<string>(input);
                Console.WriteLine($"Resposta: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
            }

            Console.WriteLine();
        }

    }
}
