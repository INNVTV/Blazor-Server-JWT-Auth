using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorServerJWTAuth.Controllers
{
    [Route("Authentication")]
    public class AuthenticationController : Controller
    {
        private Models.Configuration.Settings _settings;
        public AuthenticationController(Models.Configuration.Settings settings)
        {
            _settings = settings;
        }

        [Route("SetCookies")]
        public IActionResult SetCookies(string jwtToken, string refreshToken, string redirectUrl)
        {
            
            HttpContext.Response.Cookies.Append(
                _settings.JWTCookieName,
                jwtToken,
                new CookieOptions()
                {
                    IsEssential = true,
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTime.UtcNow.AddHours(_settings.CookieExpirationHours),
                    SameSite = SameSiteMode.Strict
                });

            HttpContext.Response.Cookies.Append(
                _settings.RefreshTokenCookieName,
                Authentication.Encryption.StringEncryption.EncryptString(refreshToken, _settings.RefreshTokenEncryptionPassPhrase),
                new CookieOptions()
                {
                    IsEssential = true,
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTime.UtcNow.AddHours(_settings.CookieExpirationHours),
                    SameSite = SameSiteMode.Strict
                });

            return LocalRedirect(redirectUrl);
        }

        [Route("RefreshToken")]
        public IActionResult RefreshToken(string refreshToken, string redirectUrl)
        {
            /*
            HttpContext.Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(
                        new RequestCulture(culture)));
            */


            return LocalRedirect(redirectUrl);
        }

        [Route("DestroyCookies")]
        public IActionResult DestroyCookies()
        {
            HttpContext.Response.Cookies.Delete(_settings.JWTCookieName);
            HttpContext.Response.Cookies.Delete(_settings.RefreshTokenCookieName);

            return LocalRedirect("/signedout");
        }
    }
}