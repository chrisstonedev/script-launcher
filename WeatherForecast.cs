using System;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

namespace ScriptLauncher
{
    public class WeatherForecast
    {
        public DateTime Date { get; init; }
        public int TemperatureC { get; init; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        public string Summary { get; init; }
    }
}