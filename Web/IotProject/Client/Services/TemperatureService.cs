using IotProject.Client.Services.Contracts;
using IotProject.Shared;
using System.Net.Http.Json;

namespace IotProject.Client.Services;

public class TemperatureService : ITemperatureService
{
    private readonly HttpClient _httpClient;

    public TemperatureService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<HumidityData>?> GetInsideHumiditiesAsync(DateTime start, DateTime end)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<HumidityData>>($"/getHumiRange?start={start}&end={end}");
    }

    public async Task<IEnumerable<TemperatureData>?> GetInsideTemperaturesAsync(DateTime start, DateTime end)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<TemperatureData>>($"/getTempRange?start={start}&end={end}");
    }

    public async Task<TempHumiDto?> GetCurrentTemperatureAndHumidityAsync()
    {
        return await _httpClient.GetFromJsonAsync<TempHumiDto>("/getCurrent");
    }

    public async Task<DateTime?> GetOldestAvailableDateAsync()
    {
        return await _httpClient.GetFromJsonAsync<DateTime>("/getOldestDate");
    }
}