using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Traccaradora.Web.Store.Ui
{
    public record UiState
    {
        public bool IsMainMenuVisible { get; init; }
    }

    public record SetMainMenuVisibilityAction
    {
        public bool Visible { get; init; }
    }

    public class UiStateFeature : Feature<UiState>
    {
        public override string GetName()
        {
            return "Ui";
        }

        protected override UiState GetInitialState()
        {
            return new UiState() { IsMainMenuVisible = true };
        }
    }

    public static class Reducers
    {
        [ReducerMethod]
        public static UiState ReduceSetMainMenuVisibilityAction(UiState state, SetMainMenuVisibilityAction action) =>
            state with { IsMainMenuVisible = action.Visible };
    }
}
