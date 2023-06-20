using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace PusulaCCProject.WebUI.AuthenticationHandler
{
    public class MyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public IServiceProvider ServiceProvider { get; set; }

        public MyAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IServiceProvider serviceProvider) : base(options, logger, encoder, clock)
        {
            ServiceProvider = serviceProvider;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            throw null;
        }
    }
}
