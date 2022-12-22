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
            .AddHttpClient<MetaWeatherClient>(client => client.BaseAddress = new Uri(host.Configuration["MetaWeather"]));

        #endregion IHost

        private static async Task Main(string[] args)
        {
            using var host = Hosting;
            await host.StartAsync();

            var weather = Services.GetRequiredService<MetaWeatherClient>();
            var location = await weather.GetLocationByName("Yekaterinburg");

            Console.WriteLine("Completed!");
            Console.Read();
            await host.StopAsync();
        }
    }
}