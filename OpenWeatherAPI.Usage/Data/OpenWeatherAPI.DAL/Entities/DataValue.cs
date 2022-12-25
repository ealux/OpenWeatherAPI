using OpenWeatherAPI.DAL.Entities.Base;
using System;

namespace OpenWeatherAPI.DAL.Entities
{
    public class DataValue : Entity
    {
        public DateTimeOffset Time { get; set; } = DateTimeOffset.Now;

        public string Value { get; set; }

        public DataSource Source { get; set; }

        public bool IsFault { get; set; }
    }
}
