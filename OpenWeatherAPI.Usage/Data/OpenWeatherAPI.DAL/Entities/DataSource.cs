using Microsoft.EntityFrameworkCore;
using OpenWeatherAPI.DAL.Entities.Base;

namespace OpenWeatherAPI.DAL.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class DataSource : NamedEntity
    {
        public string Description { get; set; }
    }
}