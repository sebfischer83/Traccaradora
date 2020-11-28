using Blazored.LocalStorage;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Traccaradora.Web.Data;
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
            //if (!(await LocalStorageService.ContainKeyAsync("UserData")))
            //{
            //    NavigationManager.NavigateTo("/Login");
            //}
            //else
            //{
            //    var data = await LocalStorageService.GetItemAsync<UserData>("UserData");
            //    Dispatcher.Dispatch(new LoginAction() { UserName = data.UserName, Password = data.Password, ServerUrl = data.ServerUrl });
            //    NavigationManager.NavigateTo("/Dashboard");
            //}
            //NavigationManager.LocationChanged += NavigationManager_LocationChanged;
        }

        private void NavigationManager_LocationChanged(object sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
        {
           
        }
    }
}
