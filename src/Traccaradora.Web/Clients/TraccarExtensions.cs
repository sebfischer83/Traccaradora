using Fluxor;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Traccaradora.Web.Store.Auth;

namespace Traccaradora.Web.Clients
{
    public static class TraccarExtensions
    {
        public static async Task<bool> CheckConnectionAsync(HttpClient httpClient, string userName, string password, string serverUrl)
        {
            var byteArray = Encoding.ASCII.GetBytes($"{userName}:{password}");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            Client traccar = new Client(httpClient);
            var baseUrl = serverUrl;

            traccar.BaseUrl = MakeApiUrl(baseUrl);

            try
            {
                var result = await traccar.DevicesGetAsync(null, null, null, null);
                if (result.StatusCode != (int)HttpStatusCode.OK)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        private static string MakeApiUrl(string url)
        {
            if (!url.EndsWith("api"))
            {
                return Flurl.Url.Combine(url, "/api");    
            }
            return url;
        }

        public static IServiceCollection AddTraccarClient(this IServiceCollection services)
        {
            services.AddScoped<Client>(provider =>
            {
                var client = provider.GetService<HttpClient>();
                var state = provider.GetService<IState<AuthState>>();
                var byteArray = Encoding.ASCII.GetBytes($"{state.Value.UserName}:{state.Value.Password}");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                Client traccar = new Client(client);
                traccar.BaseUrl = MakeApiUrl(state.Value.ServerUrl);
                return traccar;
            });

            return services;
        }
    }
}
