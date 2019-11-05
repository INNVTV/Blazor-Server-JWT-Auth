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
        [Route("login")]
        public IActionResult Login(string jwtToken, string redirectUrl)
        {
            /*
            HttpContext.Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(
                        new RequestCulture(culture)));
            */
            

            return LocalRedirect(redirectUrl);
        }

        [Route("refresh")]
        public IActionResult Refresh(string jwtToken, string redirectUrl)
        {
            /*
            HttpContext.Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(
                        new RequestCulture(culture)));
            */


            return LocalRedirect(redirectUrl);
        }
    }
}