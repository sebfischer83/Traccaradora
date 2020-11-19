#nullable enable
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Traccaradora.Web.Store.Auth
{
    public record AuthState
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? ServerUrl { get; set; }

        public AuthState(string? userName, string? password, string? serverUrl)
        {
            UserName = userName;
            Password = password;
            ServerUrl = serverUrl;
        }
    }
}
