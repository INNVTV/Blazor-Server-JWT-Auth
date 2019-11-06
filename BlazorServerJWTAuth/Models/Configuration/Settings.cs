using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerJWTAuth.Models.Configuration
{
    public class Settings
    {
        public Settings(IConfiguration configuration)
        {
            IdentityServiceUri = configuration.GetSection("IdentityServiceUri").Value;
            PublicKeyXmlString = configuration.GetSection("PublicKeyXmlString").Value;
            
            JwtIssuer = configuration.GetSection("JwtIssuer").Value;
            JwtAudience = configuration.GetSection("JwtAudience").Value;


            JWTCookieName = configuration.GetSection("JwtCookieName").Value;
            RefreshTokenCookieName = configuration.GetSection("RefreshTokenCookieName").Value;
            RefreshTokenEncryptionPassPhrase = configuration.GetSection("RefreshTokenEncryptionPassPhrase").Value;
            CookieExpirationHours = Convert.ToDouble(configuration.GetSection("CookieExpirationHours").Value);
            RefreshSessionHours = Convert.ToDouble(configuration.GetSection("RefreshSessionHours").Value);
        }

        public string IdentityServiceUri { get; set; }
        public string PublicKeyXmlString { get; set; }
        public string JwtIssuer { get; set; }
        public string JwtAudience { get; set; }

        public string JWTCookieName { get; set; }
        public string RefreshTokenCookieName { get; set; }
        public string RefreshTokenEncryptionPassPhrase { get; set; }
        public double CookieExpirationHours { get; set; }
        public double RefreshSessionHours { get; set; }
    }
}
