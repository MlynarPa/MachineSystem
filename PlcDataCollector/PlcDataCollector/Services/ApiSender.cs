using PlcDataCollector.Models;
using System.Net.Http.Json;

namespace PlcDataCollector.Services;

public class ApiSender
{
    private readonly HttpClient _httpClient = new();
    private readonly string _endpoint;

    public ApiSender(string endpoint)
    {
        _endpoint = endpoint;
    }

    public async Task SendAsync(List<Machine> machines)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(_endpoint, machines);
            response.EnsureSuccessStatusCode();
            Console.WriteLine($"✅ [SENT] {machines.Count} machines at {DateTime.Now:HH:mm:ss}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Send error: {ex.Message}");
        }
    }
}
