using OpenWeatherAPI.Interfaces.Base.Entities;

namespace OpenWeatherAPI.Domain.Base
{
    public class DataSourceInfo : INamedEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}