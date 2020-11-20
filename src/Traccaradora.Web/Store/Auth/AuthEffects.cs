#nullable enable
using System.Threading;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Fluxor;
using Newtonsoft.Json;
using Traccaradora.Web.Data;
using Traccaradora.Web.Store.Data;

namespace Traccaradora.Web.Store.Auth
{
    public class AuthEffects
    {
        private readonly ILocalStorageService _localStorage;

        public AuthEffects(Blazored.LocalStorage.ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        [EffectMethod]
        public async Task HandleLogin(LoginAction action, IDispatcher dispatcher)
        {
            var userData = new UserData() { UserName = action.UserName, Password = action.Password, ServerUrl = action.ServerUrl };
            await _localStorage.SetItemAsync("UserData", userData);
            dispatcher.Dispatch(new FetchDataAction());
        }

        [EffectMethod]
        public async Task HandleLogout(LogoutAction action, IDispatcher dispatcher)
        {
            await _localStorage.RemoveItemAsync("UserData");
        }
    }
}
