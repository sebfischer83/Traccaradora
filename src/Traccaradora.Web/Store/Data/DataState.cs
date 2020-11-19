#nullable enable
using Blazored.LocalStorage;
using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Traccaradora.Web.Clients;

namespace Traccaradora.Web.Store.Data
{
    public record DataState
    {
        public bool IsLoading { get; init; }
        public List<Clients.Device>? Devices { get; init; }
    }

    public class FetchDataAction
    {
    }

    public record FetchDataFinishAction
    {
        public List<Clients.Device>? Devices { get; init; }
    }

    public class DataFeature : Feature<DataState>
    {
        public override string GetName()
        {
            return "Data";
        }

        protected override DataState GetInitialState()
        {
            return new DataState() { IsLoading = false, Devices = null };
        }
    }

    public static class Reducers
    {
        [ReducerMethod]
        public static DataState ReduceLoadAction(DataState state, FetchDataAction action) =>
            new DataState() { Devices = null, IsLoading = true };

        [ReducerMethod]
        public static DataState ReduceLoadFinishAction(DataState state, FetchDataFinishAction action) =>
            new DataState() { IsLoading = false, Devices = action.Devices };
    }

    public class AuthEffects
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IServiceProvider _serviceProvider;

        public AuthEffects(Blazored.LocalStorage.ILocalStorageService localStorage, IServiceProvider serviceProvider)
        {
            _localStorage = localStorage;
            this._serviceProvider = serviceProvider;
        }

        [EffectMethod]
        public async Task HandleFetchData(FetchDataAction action, IDispatcher dispatcher)
        {
            var client = _serviceProvider.GetService<Client>();
            if (client == null)
            {
                return;
            }

            var devices = await client.DevicesGetAsync(null, null, null, null);

            Console.WriteLine("HandleFetchData");
            dispatcher.Dispatch(new FetchDataFinishAction() { Devices = devices.Result.ToList() });
        }
    }

}
