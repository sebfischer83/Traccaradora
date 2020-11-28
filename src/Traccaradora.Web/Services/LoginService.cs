using Blazored.LocalStorage;
using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Traccaradora.Web.Clients;
using Traccaradora.Web.Data;
using Traccaradora.Web.Store.Auth;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Traccaradora.Web.Auth;

namespace Traccaradora.Web.Services
{
    public class LoginService
    {
        private HttpClient HttpClient;
        public LoginService(IServiceProvider serviceProvider, IDispatcher dispatcher, IState<AuthState> state, 
            ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider)
        {
            ServiceProvider = serviceProvider;
            Dispatcher = dispatcher;
            State = state;
            LocalStorageService = localStorageService;
            AuthenticationStateProvider = authenticationStateProvider;
            HttpClient = (HttpClient)ServiceProvider.GetService<HttpClient>();
        }

        public Task LogoutAsync()
        {
            ((AuthProvider)AuthenticationStateProvider).MarkUserAsLoggedOut();
            return Task.CompletedTask;
        }

        public async Task<bool> Login(UserData userData)
        {
            try
            {
                if (await TraccarExtensions.CheckConnectionAsync(HttpClient, userData.UserName, userData.Password, userData.ServerUrl))
                {
                    await LocalStorageService.SetItemAsync("UserData", userData);
                    ((AuthProvider)AuthenticationStateProvider).MarkUserAsLoggedIn(userData);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            
            return false;
        }

        public IServiceProvider ServiceProvider { get; }
        public IDispatcher Dispatcher { get; }
        public IState<AuthState> State { get; }
        public ILocalStorageService LocalStorageService { get; }
        public AuthenticationStateProvider AuthenticationStateProvider { get; }
    }
}
