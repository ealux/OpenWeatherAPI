using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System;
namespace OpenWeatherAPI.BlazorUI.Infrastructure
{
    internal static class ServicesExtensions
    {
        public static IHttpClientBuilder AddApi<IInterface, IClient>(this IServiceCollection services, string address)
            where IInterface : class
            where IClient : class, IInterface
            => services
            .AddHttpClient<IInterface, IClient>(
                (host, client) => client.BaseAddress = new(host.GetRequiredService<IWebAssemblyHostEnvironment>().BaseAddress + address));
    }
}
