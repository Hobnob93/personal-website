using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using PersonalWebsite.Shared.Interfaces;
using PersonalWebsite.Shared.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Fluxor;

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
            builder.Services.AddFluxor(o =>
            {
#if DEBUG
                o.ScanAssemblies(typeof(Program).Assembly)
                    .UseReduxDevTools();
#else
                o.ScanAssemblies(typeof(Program).Assembly);
#endif
            });

            builder.Services.AddSingleton<IBoardFactory, BoardFactory>();
            builder.Services.AddSingleton<IBoardService, GameOfLifeBoard>();

            await builder.Build().RunAsync();
        }
    }
}
