using Microsoft.Extensions.DependencyInjection;
using OpenWeatherAPI.WPF.ViewModels;

namespace OpenWeatherAPI.WPF
{
    internal class ServiceLocator
    {
        public MainWindowViewModel MainModel => App.Services.GetRequiredService<MainWindowViewModel>();
    }
}