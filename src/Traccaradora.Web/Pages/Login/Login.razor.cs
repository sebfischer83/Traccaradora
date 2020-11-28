using Blazored.LocalStorage;
using Blazorise;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Traccaradora.Web.Clients;
using Traccaradora.Web.Data;
using Microsoft.Extensions.DependencyInjection;
using Traccaradora.Web.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace Traccaradora.Web.Pages.Login
{
    public partial class Login
    {
        [Inject]
        private IStringLocalizer<Login> Loc { get; set; }

        [Inject]
        private LoginService LoginService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        private Validations validations;

        private UserData UserData = new UserData();
        private Alert alert;

        void HideAlert()
        {
            alert.Hide();
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var auth = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            if (auth.User.Identity.IsAuthenticated)
                NavigationManager.NavigateTo("/Dashboard");
        }

        async Task SubmitAsync()
        {
            if (validations.ValidateAll())
            {
                validations.ClearAll();
                if (await LoginService.Login(UserData))
                {
                    NavigationManager.NavigateTo("/Dashboard");
                }
                else
                {
                    alert.Show();
                }
            }
        }
    }
}
