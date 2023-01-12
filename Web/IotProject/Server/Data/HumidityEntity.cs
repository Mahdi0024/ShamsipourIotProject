using IotProject.Shared;

namespace IotProject.Server.Data;

public class HumidityEntity : HumidityData
{
    public int Id { get; set; }

    public HumidityEntity()
    {
        DateTime = DateTime.UtcNow;
    }
}