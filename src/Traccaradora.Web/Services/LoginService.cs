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

namespace Traccaradora.Web.Services
{
    public class LoginService
    {
        private HttpClient HttpClient;
        public LoginService(IServiceProvider serviceProvider, IDispatcher dispatcher, IState<AuthState> state, ILocalStorageService localStorageService)
        {
            ServiceProvider = serviceProvider;
            Dispatcher = dispatcher;
            State = state;
            LocalStorageService = localStorageService;
            HttpClient = (HttpClient)ServiceProvider.GetService<HttpClient>();
        }

        public Task LogoutAsync()
        {
            Dispatcher.Dispatch(new LogoutAction());
            return Task.CompletedTask;
        }

        public async Task<bool> Login(UserData userData)
        {
            try
            {
                if (await TraccarExtensions.CheckConnectionAsync(HttpClient, userData.UserName, userData.Password, userData.ServerUrl))
                {
                    Dispatcher.Dispatch(new LoginAction() { UserName = userData.UserName, Password = userData.Password, ServerUrl = userData.ServerUrl });
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
    }
}
