using MSMS.Models.Login;

namespace MSMS.Services;

public class AccountService
{
    private readonly IHttpClientFactory _httpClientFactory;


    public AccountService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<Account>> GetAllAccounts()
    {
        var client = _httpClientFactory.CreateClient("EMSClient");
        var response = await client.GetAsync("/api/accountapi/getall");
        response.EnsureSuccessStatusCode();
        var accounts = await response.Content.ReadFromJsonAsync<IEnumerable<Account>>();
        return accounts!;
    }
}
