using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenWeatherAPI.DAL.Entities;
using OpenWeatherAPI.Interfaces.Base.Repositories;
using OpenWeatherAPI.WebAPIClients.Repositories;
using Polly;
using Polly.Extensions.Http;

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
            .AddHttpClient<IRepository<DataSource>, WebRepository<DataSource>>(client =>
                client.BaseAddress = new Uri($"{host.Configuration["WebApi"]}/api/DataSources/"))
            //.AddHttpClient<OpenWeatherAPIClient>(client => client.BaseAddress = new Uri(host.Configuration["OpenWeatherBaseAddress"]))
            //.SetHandlerLifetime(TimeSpan.FromMinutes(30))
            //.AddPolicyHandler(GetRetryPolicy())
        ;

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

        private static async Task Main()
        {
            using var host = Hosting;
            await host.StartAsync();

            //var weather = Services.GetRequiredService<OpenWeatherAPIClient>();

            //// Location
            //var ekb = await weather.GetLocation("Yekaterinburg");

            //// Forecast
            //var info = await weather.GetForecastData((ekb[0].Latitude, ekb[0].Longitude));
            //var info2 = await weather.GetForecastData(ekb[0].Name);

            //// Current
            //var current = await weather.GetCurrentData(ekb[0].Name);


            // WebRepository
            // Get repository
            var repo = Services.GetService<IRepository<DataSource>>();

            //// Get all sources
            //var sources = await repo.GetAll();
            //foreach (var item in sources)
            //    Console.WriteLine($"{item.Id}: {item.Name} - {item.Description}");

            //// Get sources with skip and count
            //sources = await repo.Get(skip:3, count:5);
            //foreach (var item in sources)
            //    Console.WriteLine($"{item.Id}: {item.Name} - {item.Description}");

            //// Get sources with pages
            //var page = await repo.GetPage(pageIndex: 4, pageSize: 3);
            //Console.WriteLine($"\nPage index: {page.PageIndex}" +
            //    $"\nPage size: {page.PageSize}" +
            //    $"\nTotal count: {page.TotalCount}" +
            //    $"\nTotal pages Count: {page.TotalPagesCount}");

            // Add and Update data
            var count = await repo.GetCount();
            Console.WriteLine($">>>>>>>>>> Elements in repository at operation START: {count}");

            //// Add 
            //var added_source = await repo.Add(new DataSource
            //{
            //    Name = $"foo {DateTime.Now:HH-mm-ss}",
            //    Description = "Somthing to descript from console client"
            //});

            //// Update
            //var updated = await repo.Update(new DataSource
            //{
            //    Id = 6,
            //    Name = "New from console client",
            //    Description = "BARFOOBAR"
            //});

            //// Delete
            //var deleted = await repo.Delete((await repo.Get(0, 1).ConfigureAwait(false)).Single());

            count = await repo.GetCount();
            Console.WriteLine($">>>>>>>>>> Elements in repository at operation END: {count}");

            var items = await repo.GetAll();
            foreach (var item in items)
                Console.WriteLine($"{item.Id}: {item.Name} - {item.Description}");

            Console.WriteLine(">>>>>>>>>> Completed!");
            Console.Read();
            await host.StopAsync();
        }
    }
}