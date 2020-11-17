#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Fluxor;

namespace Traccaradora.Web.Store.Auth
{
    public record AuthState
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? ServerUrl { get; set; }

        public AuthState(string? userName, string? password, string? serverUrl)
        {
            UserName = userName;
            Password = password;
            ServerUrl = serverUrl;
        }
    }

    public class AuthFeature : Feature<AuthState>
    {
        public override string GetName()
        {
            return "Auth";
        }

        protected override AuthState GetInitialState()
        {
            return new AuthState(string.Empty, string.Empty, string.Empty);
        }
    }

    public class LoginAction
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? ServerUrl { get; set; }
    }

    public class LogoutAction
    {
    }

    public static class Reducers
    {
        [ReducerMethod]
        public static AuthState ReduceLoginAction(AuthState state, LoginAction action) =>
            new AuthState(action.UserName, action.Password, action.ServerUrl);

        [ReducerMethod]
        public static AuthState ReduceLogoutAction(AuthState state, LogoutAction action) =>
            new AuthState(string.Empty, String.Empty, String.Empty);
    }

    public class AuthEffects
    {
        private readonly ISyncLocalStorageService _localStorage;

        public AuthEffects(Blazored.LocalStorage.ISyncLocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        [EffectMethod]
        public async Task HandleFetchDataAction(LogoutAction action, IDispatcher dispatcher)
        {
            _localStorage.RemoveItem("");
        }
    }
}
