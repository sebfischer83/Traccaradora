#nullable enable
using Fluxor;

namespace Traccaradora.Web.Store.Auth
{
    public class AuthFeature : Feature<AuthState>
    {
        public override string GetName()
        {
            return "Auth";
        }

        protected override AuthState GetInitialState()
        {
            return new AuthState(string.Empty, string.Empty, string.Empty);
        }
    }
}
