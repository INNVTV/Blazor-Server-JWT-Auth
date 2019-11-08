using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace BlazorServerJWTAuth.Authentication.Models
{
    /* NOTES:
    *
    *  1. It's important to implement IDisposable.
    *     The DI will need to dispose of UserIdentity properly.
    *     Especially when server pre-rendering is turned on which may instantiate multple instances.
    *  
    *  2. The Login method only runs if the instance is not already authenticated.
    *     This ensures that KeepSession() is not run multiple times on the same instance.
    *     If you need to log a user back in make sure they are signed out first, otherwise get a refresh token
    *     
    *  3. The Refresh method only runs if the instance is  authenticated.
    *     This ensures that you do not refresh a user that has not properly signed in.
    */

    public class UserIdentity : IDisposable
    {
        private double SessionRefreshHours {get; set;}
        private DateTime TokenExpirationTime { get; set; }
        public string BearerToken { get; private set; }
        public string Id { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public bool IsAuthenticated { get; private set; }
        public List<string> Roles { get; private set; }

        public UserIdentity()
        {
            IsAuthenticated = false;
            Roles = new List<string>();
            SessionRefreshHours = 0;
        }

        public void Login(string bearerToken, IEnumerable<Claim> claims, DateTime tokenExpirationTime,  double sessionRefreshHours)
        {

            if (!IsAuthenticated)
            {
                BearerToken = bearerToken;
                Id = claims.FirstOrDefault(c => c.Type.ToLower() == "id").Value.ToString();
                UserName = claims.FirstOrDefault(c => c.Type.ToLower() == "username").Value.ToString();
                Email = claims.FirstOrDefault(c => c.Type.ToLower() == "emailaddress").Value.ToString();
                Roles = claims.FirstOrDefault(c => c.Type.ToLower() == "platform-roles").Value.Split(",").ToList();

                IsAuthenticated = true;

                TokenExpirationTime = tokenExpirationTime;
                SessionRefreshHours = sessionRefreshHours;
                KeepSession();
            }
        }

        private async void KeepSession()
        {
            while(IsAuthenticated)
            {
                await Task.Delay(Convert.ToInt32(SessionRefreshHours * 60 * 60 * 1000));

                if(IsAuthenticated) //<-- Additional check in case disposal or signout occurs during delay
                {
                    Console.WriteLine("Checking if token refresh is required...");

                    if(TokenExpirationTime <= DateTime.UtcNow.AddHours(SessionRefreshHours))
                    {
                        Console.WriteLine(" > Refresh required...");

                    }
                    else
                    {
                        Console.WriteLine(" > No refresh required...");
                    }
                }  
            }
        }

        public void Dispose()
        {
            BearerToken = String.Empty;
            Id = String.Empty;
            UserName = String.Empty;
            Email = String.Empty;
            Roles = new List<string>();

            IsAuthenticated = false;
        }
    }
}
