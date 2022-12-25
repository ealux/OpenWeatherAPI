using OpenWeatherAPI.Interfaces.Base.Entities;

namespace OpenWeatherAPI.DAL.Entities.Base
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}