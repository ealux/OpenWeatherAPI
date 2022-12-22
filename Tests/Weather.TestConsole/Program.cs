using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenWeatherAPI;
using System;
using System.Threading.Tasks;

namespace Weather.TestConsole
{
    internal class Program
    {
        #region IHost

        private static IHost __Hosting;

        public static IHost Hosting => __Hosting
            ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        // Services
        public static IServiceProvider Services => Hosting.Services;

        // Create host builder
        public static IHostBuilder CreateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)
            .ConfigureServices(ConfigureServices);

        // Create services
        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services) => services
            .AddHttpClient<WeatherClient>(client => client.BaseAddress = new Uri(host.Configuration["OpenWeather"]));

        #endregion IHost

        private static async Task Main()
        {
            using var host = Hosting;
            await host.StartAsync();

            var weather = Services.GetRequiredService<WeatherClient>();

            // Location
            var ekb = await weather.GetLocation("Paris");

            // Forecast
            var info = await weather.GetLocationForecastData((ekb[0].Latitude, ekb[0].Longitude));
            var info2 = await weather.GetLocationForecastData(ekb[0].Name);

            // Current
            var current = await weather.GetLocationCurrentData(ekb[0].Name);

            Console.WriteLine("Completed!");
            Console.Read();
            await host.StopAsync();
        }
    }
}