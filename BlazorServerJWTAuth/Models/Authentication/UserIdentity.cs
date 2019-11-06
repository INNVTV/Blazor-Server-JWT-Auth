using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerJWTAuth.Models.Authentication
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
        public UserIdentity()
        {
            IsAuthenticated = false;
            Roles = new List<string>();
        }

        public void Login(string id, string userName, string email, List<string> roles)
        {
            if(!IsAuthenticated)
            {
                Id = id;
                UserName = userName;
                Email = email;
                Roles = roles;

                IsAuthenticated = true;
                KeepSession();
            }
        }

        public void Refresh(string id, string userName, string email, List<string> roles)
        {
            if(IsAuthenticated)
            {
                Id = id;
                UserName = userName;
                Email = email;
                Roles = roles;
            }

        }

        public void Logout()
        {
            Id = String.Empty;
            UserName = String.Empty;
            Email = String.Empty;
            Roles = new List<string>();

            Dispose();
        }

        public string Id { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public bool IsAuthenticated { get; private set; }
        public List<string> Roles { get; private set; }

        private async void KeepSession()
        {
            int count = 0;

            while(IsAuthenticated)
            {
                await Task.Delay(9000);
                count++;
                Console.WriteLine($"({count}) id:({Id}) Checking refresh token...");
            }
        }

        public void Dispose()
        {
            IsAuthenticated = false;
        }
    }
}
