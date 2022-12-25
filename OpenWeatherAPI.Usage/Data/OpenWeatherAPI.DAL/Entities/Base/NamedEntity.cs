using OpenWeatherAPI.Interfaces.Base.Entities;

namespace OpenWeatherAPI.DAL.Entities.Base
{
    public abstract class NamedEntity : Entity, INamedEntity
    {
        public string Name { get; set; }
    }
}