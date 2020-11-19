using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Fluxor;
using Traccaradora.Web.Clients;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using System.Globalization;
using Traccaradora.Web.Services;

namespace Traccaradora.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            var currentAssembly = typeof(Program).Assembly;
            builder.Services.AddFluxor(options => options.ScanAssemblies(currentAssembly).UseReduxDevTools());
            builder.Services.AddScoped(sp => new HttpClient());
            builder.Services
              .AddBlazorise(options =>
              {
                  options.ChangeTextOnKeyPress = true;
              })
              .AddBootstrapProviders()
              .AddFontAwesomeIcons();
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTraccarClient();
            builder.Services.AddLocalization();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddBlazoredSessionStorage();
            // services app
            builder.Services.AddScoped<LoginService>();

            var host = builder.Build();

            host.Services
              .UseBootstrapProviders()
              .UseFontAwesomeIcons();

            //var culture = new CultureInfo("en-US");
            //CultureInfo.DefaultThreadCurrentCulture = culture;
            //CultureInfo.DefaultThreadCurrentUICulture = culture;

            await host.RunAsync();
        }
    }
}
