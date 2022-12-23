using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenWeatherAPI;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OpenWeatherAPI.ConsoleUI
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
            .AddHttpClient<OpenWeatherAPIClient>(client => client.BaseAddress = new Uri(host.Configuration["OpenWeatherBaseAddress"]))
            .SetHandlerLifetime(TimeSpan.FromMinutes(30))
            .AddPolicyHandler(GetRetryPolicy());

        #region [Http policy]

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            var jitter = new Random();
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(5, retry_atempt =>
                    TimeSpan.FromSeconds(Math.Pow(2, retry_atempt)) +
                    TimeSpan.FromMilliseconds(jitter.Next(0, 1000)));
        }

        #endregion [Http policy]

        #endregion IHost

        static async Task Main()
        {
            using var host = Hosting;
            await host.StartAsync();

            var weather = Services.GetRequiredService<OpenWeatherAPIClient>();

            // Location
            var ekb = await weather.GetLocation("Yekaterinburg");

            // Forecast
            var info = await weather.GetForecastData((ekb[0].Latitude, ekb[0].Longitude));
            var info2 = await weather.GetForecastData(ekb[0].Name);

            // Current
            var current = await weather.GetCurrentData(ekb[0].Name);

            Console.WriteLine("Completed!");
            Console.Read();
            await host.StopAsync();
        }
    }
}