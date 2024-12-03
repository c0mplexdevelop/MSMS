using MSMS.Models.Diagnosis;
using MSMS.Models.Procedures;

namespace MSMS.Services;

public class PatientServices
{
    private readonly IHttpClientFactory _httpClientFactory;
    

    public PatientServices(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<Patient>> GetAllPatients()
    {
        var client = _httpClientFactory.CreateClient("PMSClient");
        var response = await client.GetAsync("/api/patients/getall");
        response.EnsureSuccessStatusCode();
        var patients = await response.Content.ReadFromJsonAsync<IEnumerable<Patient>>();
        return patients!;
    }
}
