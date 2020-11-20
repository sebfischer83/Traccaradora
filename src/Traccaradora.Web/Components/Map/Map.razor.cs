using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Traccaradora.Web.Store.Data;

namespace Traccaradora.Web.Components.Map
{
    public partial class Map : ComponentBase, IAsyncDisposable
    {
        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        [Inject]
        private IState<DataState> State { get; set; }

        private Task<IJSObjectReference> _module;
        private Task<IJSObjectReference> Module => _module ??= JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/Map.js").AsTask();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            State.StateChanged += State_StateChanged;
        }

        private void State_StateChanged(object sender, DataState e)
        {
            InvokeAsync(StateHasChanged);
            if (e.IsInitialized)
            {
                Task.Run(async () =>
                {
                    var module = await Module;
                    await module.InvokeVoidAsync("initMap");
                });
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_module != null)
            {
                var module = await _module;
                await module.DisposeAsync();
            }
            State.StateChanged -= State_StateChanged;
        }
    }
}
