using Fluxor;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Traccaradora.Web.Store.Auth;

namespace Traccaradora.Web.Components.Header
{
    public partial class Header : Fluxor.Blazor.Web.Components.FluxorComponent
    {
        [Inject]
        private IState<AuthState> AuthState { get; set; }
    }
}
