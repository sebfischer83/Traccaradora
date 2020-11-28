using Blazored.LocalStorage;
using Fluxor;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Traccaradora.Web.Data;
using Traccaradora.Web.Store.Auth;

namespace Traccaradora.Web.Auth
{
    public class AuthProvider : AuthenticationStateProvider
    {
        private readonly IState<AuthState> _authState;
        private readonly IDispatcher _dispatcher;
        private readonly ILocalStorageService _localStorageService;

        public AuthProvider(IState<AuthState> authState, IDispatcher dispatcher, ILocalStorageService localStorageService)
        {
            this._authState = authState;
            this._dispatcher = dispatcher;
            this._localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsIdentity identity;
            if (!(await _localStorageService.ContainKeyAsync("UserData")))
            {
                identity = new ClaimsIdentity(null, "");
            }
            else
            {
                var data = await _localStorageService.GetItemAsync<UserData>("UserData");
                _dispatcher.Dispatch(new LoginAction() { UserName = data.UserName, Password = data.Password, ServerUrl = data.ServerUrl });
                identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, data.UserName), new Claim(ClaimTypes.Email, data.UserName) }, "traccarlogin");
            }
            
            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }

        public void MarkUserAsLoggedIn(UserData data)
        {
            ClaimsIdentity identity;
            _dispatcher.Dispatch(new LoginAction() { UserName = data.UserName, Password = data.Password, ServerUrl = data.ServerUrl });
            identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, data.UserName), new Claim(ClaimTypes.Email, data.UserName) }, "traccarlogin");
            var user = new ClaimsPrincipal(identity);
            var state = Task.FromResult(new AuthenticationState(user));
            NotifyAuthenticationStateChanged(state);
        }

        public void MarkUserAsLoggedOut()
        {
            _dispatcher.Dispatch(new LogoutAction());
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
