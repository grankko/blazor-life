using BlazorLife.Client.Interop;
using BlazorLife.Client.ViewModels;
using BlazorLife.Game;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorLife.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton<GameService>();
            builder.Services.AddSingleton<GameViewModel>();
            builder.Services.AddSingleton<JavascriptService>();
            await builder.Build().RunAsync();

        }
    }
}