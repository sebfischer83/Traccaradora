#nullable enable

namespace Traccaradora.Web.Store.Auth
{
    public class LoginAction
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? ServerUrl { get; set; }
    }
}
