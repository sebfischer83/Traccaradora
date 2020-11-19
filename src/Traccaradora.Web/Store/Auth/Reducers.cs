#nullable enable
using System;
using Fluxor;

namespace Traccaradora.Web.Store.Auth
{
    public static class Reducers
    {
        [ReducerMethod]
        public static AuthState ReduceLoginAction(AuthState state, LoginAction action) =>
            new AuthState(action.UserName, action.Password, action.ServerUrl);

        [ReducerMethod]
        public static AuthState ReduceLogoutAction(AuthState state, LogoutAction action) =>
            new AuthState(string.Empty, String.Empty, String.Empty);
    }
}
