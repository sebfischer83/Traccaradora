using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Traccaradora.Web.Store.Auth;
using Traccaradora.Web.Store.Ui;

namespace Traccaradora.Web.Components.Header
{
    public partial class Header : Fluxor.Blazor.Web.Components.FluxorComponent
    {
        [Inject]
        private IState<AuthState> AuthState { get; set; }

        [Inject]
        private IState<UiState> UiState { get; set; }

        [Inject]
        private IDispatcher Dispatcher { get; set; }

        private void ToggleMainMenu(MouseEventArgs e)
        {
            var visible = UiState.Value.IsMainMenuVisible;
            Dispatcher.Dispatch(new Traccaradora.Web.Store.Ui.SetMainMenuVisibilityAction() { Visible = !visible });
        }
    }
}
