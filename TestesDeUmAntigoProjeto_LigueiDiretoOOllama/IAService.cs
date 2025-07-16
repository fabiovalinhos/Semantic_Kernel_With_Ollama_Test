using System.Text;
using System.Text.Json;

public class IAService
{
    private readonly HttpClient _http;
    // private readonly List<(string Nome, MCPClient Cliente)> _mcps;

    public IAService()
    {
        _http = new HttpClient { BaseAddress = new Uri("http://localhost:11434") };

        // _mcps = new List<(string, MCPClient)>
        // {
        //     ("Context7", new MCPClient("http://localhost:7001")),
        // };
    }

    public async Task<string> PerguntarComTodosAsync(string pergunta)
    {
        // var contextos = await Task.WhenAll(_mcps.Select(async mcp =>
        // {
        //     var conteudo = await mcp.Cliente.GetContextAsync(pergunta);
        //     return $"\n--- {mcp.Nome} ---\n{conteudo ?? "(sem resposta)"}";
        // }));

        // var contextoUnido = string.Join("\n", contextos);

        var prompt = pergunta;

        var body = new
        {
            model = "deepseek-r1",
            prompt = prompt,
            stream = false
        };

        var jsonBody = JsonSerializer.Serialize(body);
        var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
        var response = await _http.PostAsync("/api/generate", content);
        var resultJson = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<OllamaResponse>(resultJson);
        return result?.response ?? "(sem resposta)";
    }

    private class OllamaResponse
    {
        public string? response { get; set; }
    }
}
