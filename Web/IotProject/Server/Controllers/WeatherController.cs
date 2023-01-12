using IotProject.Server.Data;
using IotProject.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IotProject.Server.Controllers;

public class WeatherController : ControllerBase
{
    private readonly IotDbContext _db;

    public WeatherController(IotDbContext db)
    {
        _db = db;
    }

    [HttpGet("/send")]
    public async Task<IActionResult> SendEndpoint(float temp, int humi)
    {
        await _db.Temperatures.AddAsync(new() { Temperature = temp });
        await _db.Humidities.AddAsync(new() { Humidity = humi });
        await _db.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("/getCurrent")]
    public async Task<IActionResult> GetCurrent()
    {
        var temperature = await _db.Temperatures
            .OrderByDescending(t => t.DateTime)
            .Select(t => t.Temperature)
            .FirstOrDefaultAsync();
        var humidity = await _db.Humidities
            .OrderByDescending(t => t.DateTime)
            .Select(t => t.Humidity)
            .FirstOrDefaultAsync();
        return Ok(new TempHumiDto { Temperature = temperature, Humidity = humidity });
    }

    [HttpGet("/getTempRange")]
    public async Task<IActionResult> GetTemperatureRange(DateTime start, DateTime? end)
    {
        end ??= DateTime.UtcNow;
        if (start > end)
            return BadRequest("Start was sooner than end!");

        var insideTemperatures = await _db.Temperatures
            .Where(t => t.DateTime >= start && t.DateTime <= end)
            .OrderBy(t => t.DateTime)
            .ToListAsync();

        return Ok(insideTemperatures.Cast<TemperatureData>());
    }

    [HttpGet("/getHumiRange")]
    public async Task<IActionResult> GetHumidityRange(DateTime start, DateTime? end)
    {
        end ??= DateTime.UtcNow;
        if (start > end)
            return BadRequest("Start was sooner than end!");

        var insideHumiditys = await _db.Humidities
            .Where(h => h.DateTime >= start && h.DateTime <= end)
            .OrderBy(h => h.DateTime)
            .ToListAsync();

        return Ok(insideHumiditys.Cast<HumidityData>());
    }

    [HttpGet("/getOldestDate")]
    public async Task<IActionResult> GetOldestDate()
    {
        var oldestDate = await _db.Temperatures.Select(t => t.DateTime).MinAsync();
        return Ok(oldestDate);
    }

    [HttpGet("clearAllData")]
    public IActionResult ClearAllData()
    {
        _db.Database.EnsureDeleted();
        _db.Database.EnsureCreated();
        return Ok("Database Fucking deleted.");
    }
}