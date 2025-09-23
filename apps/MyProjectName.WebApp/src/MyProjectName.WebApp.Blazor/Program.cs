using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyProjectName.WebApp.Blazor;
using MyProjectName.WebApp.Blazor.Client;
using Volo.Abp.AspNetCore.Components.WebAssembly.WebApp;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        //https://github.com/dotnet/aspnetcore/issues/52530
        builder.Services.Configure<RouteOptions>(options =>
        {
            options.SuppressCheckForUnhandledSecurityMetadata = true;
        });

        // Add services to the container.
        builder.Services.AddRazorComponents().AddInteractiveWebAssemblyComponents();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }

        app.UseHttpsRedirection();

        app.MapStaticAssets();
        //app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(
                WebAppAdditionalAssembliesHelper.GetAssemblies<WebAppBlazorClientModule>()
            );

        await app.RunAsync();
    }
}
