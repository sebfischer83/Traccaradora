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

        private Validations validations;

        private UserData UserData = new UserData();
        private Alert alert;

        void HideAlert()
        {
            alert.Hide();
        }

        async Task SubmitAsync()
        {
            if (validations.ValidateAll())
            {
                validations.ClearAll();
                if (await LoginService.Login(UserData))
                {
                    NavigationManager.NavigateTo("/");
                }
                else
                {
                    alert.Show();
                }
            }
        }

        private void CheckConnection()
        {

        }
    }
}
