namespace HotTubTriMachine.Services;

public class MarkdownService
{
    private readonly HttpClient _httpClient;

    public MarkdownService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string?> GetMarkdownAsync(string filePath)
    {
        try
        {
            var response = await _httpClient.GetAsync(filePath);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
        }
        catch (Exception)
        {
            // Handle error silently, return null
        }
        return null;
    }
}
