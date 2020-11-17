using Blazored.LocalStorage;
using Fluxor;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Traccaradora.Web.Store.Auth;

namespace Traccaradora.Web
{
    public partial class App
    {
        [Inject]
        private IState<AuthState> AuthState { get; set; }

        [Inject]
        private ILocalStorageService LocalStorageService { get; set; }

        [Inject]
        public IDispatcher Dispatcher { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (!(await LocalStorageService.ContainKeyAsync("UserData")))
            {
                NavigationManager.NavigateTo("/Login");
            }
            else
            {
                Dispatcher.Dispatch(new LoginAction() { UserName = "sebfischer@gmx.net", Password = "us82qhFB", ServerUrl = "https://api.budgetari.net/api" });
            }
            NavigationManager.LocationChanged += NavigationManager_LocationChanged;
        }

        private void NavigationManager_LocationChanged(object sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
        {
            if (!e.Location.Contains("/Login"))
            {
                if (string.IsNullOrWhiteSpace(AuthState.Value.UserName))
                    NavigationManager.NavigateTo("/Login");
            }
        }
    }
}
