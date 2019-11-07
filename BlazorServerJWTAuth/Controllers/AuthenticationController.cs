using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BlazorServerJWTAuth.Controllers
{
    [Route("authentication")]
    public class AuthenticationController : Controller
    {
        private Models.Configuration.Settings _settings;
        public AuthenticationController(Models.Configuration.Settings settings)
        {
            _settings = settings;
        }

        [Route("login")]
        public IActionResult Login(string jwtToken, string refreshToken, string redirectUrl)
        {
            
            HttpContext.Response.Cookies.Append(
                _settings.JWTCookieName,
                jwtToken);

            HttpContext.Response.Cookies.Append(
                _settings.RefreshTokenCookieName,
                refreshToken);

            return LocalRedirect(redirectUrl);
        }

        [Route("refresh")]
        public IActionResult Refresh(string refreshToken, string redirectUrl)
        {
            /*
            HttpContext.Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(
                        new RequestCulture(culture)));
            */


            return LocalRedirect(redirectUrl);
        }

        [Route("signout")]
        public IActionResult Signout()
        {
            /*
            HttpContext.Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(
                        new RequestCulture(culture)));
            */


            return LocalRedirect("signout");
        }
    }
}