using BlazorLife.Client;
using BlazorLife.Client.ViewModels;
using BlazorLife.Game;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<GameService>();
        services.AddSingleton<GameViewModel>();
    }

    public void Configure(IBlazorApplicationBuilder app)
    {
        app.AddComponent<App>("app");
    }
}