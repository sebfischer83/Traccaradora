using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Traccaradora.Web.Validation
{
    public class UrlValidator
    {
        public static void IsValidUrl(Blazorise.ValidatorEventArgs e)
        {
            var url = (string)e.Value;
            if (string.IsNullOrWhiteSpace(url))
            {
                e.Status = Blazorise.ValidationStatus.Error;
                return;
            }
            e.Status = Flurl.Url.IsValid(url) ? Blazorise.ValidationStatus.Success : Blazorise.ValidationStatus.Error;
        }
    }
}
