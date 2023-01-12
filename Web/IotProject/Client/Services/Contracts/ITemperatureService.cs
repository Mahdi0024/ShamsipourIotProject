using IotProject.Shared;

namespace IotProject.Client.Services.Contracts;

public interface ITemperatureService
{
    public Task<TempHumiDto?> GetCurrentTemperatureAndHumidityAsync();

    public Task<IEnumerable<TemperatureData>?> GetInsideTemperaturesAsync(DateTime start, DateTime end);

    public Task<IEnumerable<HumidityData>?> GetInsideHumiditiesAsync(DateTime start, DateTime end);

    public Task<DateTime?> GetOldestAvailableDateAsync();
}