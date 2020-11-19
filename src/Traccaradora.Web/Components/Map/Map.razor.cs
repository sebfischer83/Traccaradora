using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Traccaradora.Web.Components.Map
{
    public partial class Map : ComponentBase, IAsyncDisposable
    {
        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        private Task<IJSObjectReference> _module;
        private Task<IJSObjectReference> Module => _module ??= JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/Map.js").AsTask();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var module = await Module;
            await module.InvokeVoidAsync("initMap");
        }

        //async Task Submit()
        //{
        //    var module = await Module;
        //    await module.InvokeVoidAsync("sayHi", name);
        //}

        public async ValueTask DisposeAsync()
        {
            if (_module != null)
            {
                var module = await _module;
                await module.DisposeAsync();
            }
        }
    }
}
