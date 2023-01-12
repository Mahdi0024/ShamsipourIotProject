using IotProject.Shared;

namespace IotProject.Server.Data
{
    public class TemperatureEntity : TemperatureData
    {
        public int Id { get; set; }

        public TemperatureEntity()
        {
            DateTime = DateTime.UtcNow;
        }
    }
}