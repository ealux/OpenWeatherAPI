using System.ComponentModel.DataAnnotations;

namespace OpenWeatherAPI.Interfaces.Base.Entities
{
    public interface INamedEntity : IEntity
    {
        [Required]
        string Name { get; }
    }
}