using System.Text;
using System.Text.Json;

public class MCPClient
{
    private readonly HttpClient _http;

    public MCPClient(string baseUrl)
    {
        _http = new HttpClient { BaseAddress = new Uri(baseUrl) };
    }

    public async Task<string> GetContextAsync(string query)
    {
        var request = new
        {
            jsonrpc = "2.0",
            id = Guid.NewGuid().ToString(),
            method = "search-library-docs",
            @params = new { query }
        };
        var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        var response = await _http.PostAsync("/mcp", content);
        response.EnsureSuccessStatusCode();

        var resultJson = await response.Content.ReadAsStringAsync();
        var json = JsonSerializer.Deserialize<MCPResponse>(resultJson);
        return json?.content;
    }

    private class MCPResponse
    {
        public string? content { get; set; }
    }
}
