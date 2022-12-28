using OpenWeatherAPI.Interfaces.Base.Entities;
using System;

namespace OpenWeatherAPI.Domain.Base
{
    public class DataValueInfo : IEntity
    {
        public int Id { get; set; }

        public DateTimeOffset Time { get; set; }

        public string Value { get; set; }

        public bool IsFault { get; set; }
    }
}