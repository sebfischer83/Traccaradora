using Fluxor;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Traccaradora.Web.Store.Auth;

namespace Traccaradora.Web.Clients
{
    public static class TraccarExtensions
    {
        public static IServiceCollection AddTraccarClient(this IServiceCollection services)
        {
            services.AddScoped<Client>(provider =>
            {
                var client = provider.GetService<HttpClient>();
                var state = provider.GetService<IState<AuthState>>();
                var byteArray = Encoding.ASCII.GetBytes($"{state.Value.UserName}:{state.Value.Password}");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                if (!string.IsNullOrWhiteSpace(state.Value.ServerUrl))
                    client.BaseAddress = new Uri(state.Value.ServerUrl);
                Client traccar = new Client(client);
                traccar.BaseUrl = state.Value.ServerUrl;
                return traccar;
            });

            return services;
        }
    }
}
