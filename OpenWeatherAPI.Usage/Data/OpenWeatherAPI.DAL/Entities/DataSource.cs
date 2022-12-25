using OpenWeatherAPI.DAL.Entities.Base;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeatherAPI.DAL.Entities
{
    public class DataSource : NamedEntity
    {
        public string Description { get; set; }
    }
}
