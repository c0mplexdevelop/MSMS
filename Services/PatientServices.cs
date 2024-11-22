using MSMS.Models.Diagnosis;
using MSMS.Models.Procedures;

namespace MSMS.Services;

public class PatientServices
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://(ip):7001/";

    public PatientServices(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Patient>> GetAllPatients()
    {
        var response = await _httpClient.GetAsync("api/patient/getall");
        response.EnsureSuccessStatusCode();
        var patients = await response.Content.ReadFromJsonAsync<IEnumerable<Patient>>();
        return patients!;
    }
}
