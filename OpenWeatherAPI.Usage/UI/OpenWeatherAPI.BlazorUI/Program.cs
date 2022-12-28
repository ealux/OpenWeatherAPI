using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OpenWeatherAPI.BlazorUI;
using OpenWeatherAPI.BlazorUI.Infrastructure;
using OpenWeatherAPI.BlazorUI.Pages;
using OpenWeatherAPI.Domain.Base;
using OpenWeatherAPI.Interfaces.Base.Repositories;
using OpenWeatherAPI.WebAPIClients.Repositories;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient
builder.Services.AddScoped(_ => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

// SourceRepositoryClient
builder.Services.AddApi<IRepository<DataSourceInfo>, WebRepository<DataSourceInfo>>("/api/SourcesRepository");

await builder.Build().RunAsync();