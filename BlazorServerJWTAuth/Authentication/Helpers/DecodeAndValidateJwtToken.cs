using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BlazorServerJWTAuth.Authentication.Helpers
{
    public static class DecodeAndValidate
    {
        public static DecodeAndValidateJwtTokenResults JwtToken(string tokenString, string xmlPublicKeyString, string validAudience, string validIssuer)
        {
            var jwtHandler = new JwtSecurityTokenHandler();

            var readableToken = jwtHandler.CanReadToken(tokenString);

            if (!readableToken)
            {
                return new DecodeAndValidateJwtTokenResults { isValid = false };
            }
            else
            {
                // Build our public siging key
                var rsaProvider = new RSACryptoServiceProvider();
                // Note: Requires the RsaCryptoExtensions.cs class in 'Helpers' folder (ToXMLString(true/flase) does not work in .Net Core so we have an extention method that parses pub/priv without boolean flag)
                rsaProvider.FromXmlRsaString_PublicOnly(xmlPublicKeyString);
                var rsaPublicKey = new RsaSecurityKey(rsaProvider);


                var jwt = new JwtSecurityToken(tokenString);


                var tokenValidationParameters = new TokenValidationParameters()
                {
                    ValidAudience = validAudience,
                    ValidIssuer = validIssuer,
                    IssuerSigningKey = rsaPublicKey,

                    ValidateLifetime = true,    //<-- default is 'true' - we validate that the token is not expired
                    ClockSkew = TimeSpan.Zero, //<-- default clock skew is 5 min. This ensures it is '0'

                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                };


                SecurityToken validatedToken;
                ClaimsPrincipal claimsPrincipal;

                var handler = new JwtSecurityTokenHandler();

                try
                {
                    claimsPrincipal = handler.ValidateToken(tokenString, tokenValidationParameters, out validatedToken);
                }
                catch (SecurityTokenExpiredException ex)
                {
                    return new DecodeAndValidateJwtTokenResults { isValid = false, isExpired = true };
                }
                catch (SecurityTokenException ex)
                {
                    return new DecodeAndValidateJwtTokenResults { isValid = false };
                }               
                catch (Exception ex)
                {
                    //Log.Critical(ex.ToString()); // Something else happened!
                    //throw;
                    return new DecodeAndValidateJwtTokenResults { isValid = false };
                }

                return new DecodeAndValidateJwtTokenResults {
                    isValid = true,
                    ClaimsPrincipal = claimsPrincipal,
                    SecurityToken = validatedToken,
                    ExpirationTime = validatedToken.ValidTo,
                    ExpirationHours = (validatedToken.ValidTo - DateTime.UtcNow).TotalHours
                };
               

            }
            
        }
    
    }
}
