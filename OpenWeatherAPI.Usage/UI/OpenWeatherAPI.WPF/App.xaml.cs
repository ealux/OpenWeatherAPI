using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenWeatherAPI.DAL.Entities;
using OpenWeatherAPI.Interfaces.Base.Repositories;
using OpenWeatherAPI.WebAPIClients.Repositories;
using OpenWeatherAPI.WPF.ViewModels;
using System;
using System.Linq;
using System.Windows;

namespace OpenWeatherAPI.WPF
{
    public partial class App
    {
        #region [Windows]

        public static Window? ActiveWindow => Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsActive);
        public static Window? FocusedWindow => Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsFocused);
        public static Window? CurrentWindow => FocusedWindow ?? ActiveWindow;

        #endregion [Windows]

        #region [Hosting]

        private static IHost __Hosting;

        public static IHost Hosting => __Hosting ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public static IServiceProvider Services => __Hosting.Services;

        // Host builder
        private static IHostBuilder CreateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)
            .ConfigureServices(ConfigureServices);

        // Services builder
        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddScoped<MainWindowViewModel>();
            services.AddHttpClient<IRepository<DataSource>, WebRepository<DataSource>>(client =>
                client.BaseAddress = new Uri($"{host.Configuration["WebApi"]}/api/DataSources/"));
        }

        #endregion [Hosting]

        protected override async void OnStartup(StartupEventArgs e)
        {
            var host = Hosting;
            base.OnStartup(e);
            await host.StartAsync().ConfigureAwait(false);
            //Services.GetRequiredService<MainWindow>().Show();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using var host = Hosting;
            base.OnExit(e);
            await Hosting.StopAsync().ConfigureAwait(false);
        }
    }
}