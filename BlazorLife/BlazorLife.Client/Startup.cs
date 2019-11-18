using BlazorLife.Client;
using BlazorLife.Client.Interop;
using BlazorLife.Client.ViewModels;
using BlazorLife.Game;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorLife.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<GameService>();
            services.AddSingleton<GameViewModel>();
            services.AddSingleton<JavascriptService>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }

}