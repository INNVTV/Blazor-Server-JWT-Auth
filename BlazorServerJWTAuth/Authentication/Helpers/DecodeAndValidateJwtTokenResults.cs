using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace BlazorServerJWTAuth.Authentication.Helpers
{
    public class DecodeAndValidateJwtTokenResults
    {
        public DecodeAndValidateJwtTokenResults()
        {
            isValid = false;
            isExpired = false;
            ExpirationHours = 0;
        }

        public bool isValid { get; set; }
        public bool isExpired { get; set; }
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
        public SecurityToken SecurityToken { get; set; }
        public double ExpirationHours { get; set; }
    }
}