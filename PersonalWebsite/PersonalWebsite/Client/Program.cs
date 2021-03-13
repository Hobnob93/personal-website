using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using PersonalWebsite.Shared.Interfaces;
using PersonalWebsite.Shared.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PersonalWebsite.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddMudServices();

            builder.Services.AddScoped<IBoardFactory, BoardFactory>();

            await builder.Build().RunAsync();
        }
    }
}
