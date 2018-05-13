using Microsoft.AspNetCore.Blazor.Browser.Rendering;
using Microsoft.AspNetCore.Blazor.Browser.Services;
using BlazorLife.Game;
using Microsoft.Extensions.DependencyInjection;
using System;
using BlazorLife.Client.ViewModels;

namespace BlazorLife.Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new BrowserServiceProvider(services =>
            {
                services.AddSingleton<GameService>();
                services.AddSingleton<GameViewModel>();
            });

            new BrowserRenderer(serviceProvider).AddComponent<App>("app");
        }
    }
}
