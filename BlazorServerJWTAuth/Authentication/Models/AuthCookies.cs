using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerJWTAuth.Authentication.Models
{
    public class AuthCookies
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
