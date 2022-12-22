using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace MetaWeather.TestConsole
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
            .AddHttpClient<WeatherClient>(client => client.BaseAddress = new Uri(host.Configuration["MetaWeather"]));

        #endregion IHost

        private static async Task Main()
        {
            using var host = Hosting;
            await host.StartAsync();

            var weather = Services.GetRequiredService<WeatherClient>();

            var ekb = await weather.GetLocation("Yekaterinburg");

            var info = await weather.GetLocationData((ekb[0].Latitude, ekb[0].Longitude));
            var info2 = await weather.GetLocationData(ekb[0].Name);

            Console.WriteLine("Completed!");
            Console.Read();
            await host.StopAsync();
        }
    }
}