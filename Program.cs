using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.Extensions.DependencyInjection;

// Criar HttpClient com timeout personalizado
var httpClient = new HttpClient
{
    Timeout = TimeSpan.FromMinutes(5)
};

// Criar o OllamaApiClient com o HttpClient customizado
// Criar o kernel builder e registrar o serviço
var builder = Kernel.CreateBuilder();
builder.Services.AddSingleton(httpClient);

builder.AddOllamaChatCompletion(
    modelId: "deepseek-r1",
    endpoint: new Uri("http://localhost:11434")
);
var kernel = builder.Build();

// Loop do chat
var chatHistory = new ChatHistory();
Console.WriteLine("Chat iniciado. Digite 'sair' para terminar.");

while (true)
{
    Console.Write("Você (digite 'sair' se deseja terminar): ");
    var userMessage = Console.ReadLine();
    if (string.Equals(userMessage, "sair", StringComparison.OrdinalIgnoreCase))
        break;
    if (string.IsNullOrWhiteSpace(userMessage))
        continue;

    chatHistory.AddUserMessage(userMessage);

    var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();
    var result = await chatCompletionService.GetChatMessageContentAsync(chatHistory);

    Console.WriteLine($"IA: {result.Content}");
    chatHistory.AddAssistantMessage(result.Content ?? string.Empty);
}



// Código alternativo para criar o serviço de chat diretamente, sem usar o Kernel
// sem usar GetRequiredService<IChatCompletionService>()

// using Microsoft.SemanticKernel;
// using Microsoft.SemanticKernel.ChatCompletion;
// using Microsoft.SemanticKernel.Connectors.Ollama;

// // Cria o serviço de chat diretamente
// var chatService = new OllamaChatCompletionService(
//     modelId: "deepseek-r1",
//     endpoint: new Uri("http://localhost:11434"),
//     serviceId: "Ollama_DeepSeek"
// );

// // Cria o Kernel e registra o serviço
// // var kernel = Kernel.CreateBuilder()
// //     .AddChatCompletionService(chatService) // forma direta
// //     .Build();

// // Cria o histórico do chat
// var chatHistory = new ChatHistory();
// Console.WriteLine("Chat iniciado. Digite 'sair' para terminar.");

// while (true)
// {
//     Console.Write("Você: ");
//     var userMessage = Console.ReadLine();

//     if (string.Equals(userMessage, "sair", StringComparison.OrdinalIgnoreCase))
//         break;

//     if (string.IsNullOrWhiteSpace(userMessage))
//         continue;

//     chatHistory.AddUserMessage(userMessage);

//     // Usa o serviço diretamente, sem resolver pelo Kernel
//     var result = await chatService.GetChatMessageContentAsync(chatHistory, kernel: kernel);

//     Console.WriteLine($"IA: {result.Content}");
//     chatHistory.AddAssistantMessage(result.Content ?? string.Empty);
// }


/// vamos testar este tutorial
/// https://tallesvaliatti.com/criando-sql-queries-com-o-semantic-kernel-parte-1-cb12c128e2ef