using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace PusulaCCProject.WebUI.AuthenticationHandler
{
   
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private const string ApiKeyHeaderName = "X-Access-Token";
        private const string AuthenticationScheme = "ApiKeyAuth";

        public ApiKeyAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock) : base(options, logger, encoder, clock)
        {

        }



        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {

            var apiKey = Request.HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(apiKey))
            {
                return Task.FromResult(AuthenticateResult.Fail("Hata"));
            }

            // API Key'i doğrulama işlemlerini burada gerçekleştirin.
            // Örneğin, apiKey değerini veritabanında veya başka bir veri kaynağında kontrol edebilirsiniz.

            // API Key doğrulandıysa, kimlik bilgilerini oluşturun ve yetkilendirme başarılı olarak döndürün.
            var claims = new[]
            {
                new Claim(AuthenticationScheme,apiKey),
                // İsteğe bağlı olarak ek kimlik bilgileri ekleyebilirsiniz.
            };
            var identity = new ClaimsIdentity(claims, AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, AuthenticationScheme);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
