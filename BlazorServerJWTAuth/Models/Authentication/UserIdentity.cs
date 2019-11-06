using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerJWTAuth.Models.Authentication
{
    public class UserIdentity : IDisposable 
    {
        public UserIdentity()
        {
            IsAuthenticated = false;
            Roles = new List<string>();
        }

        public void Login(string id, string userName, string email, List<string> roles)
        {
            Id = id;
            UserName = userName;
            Email = email;
            Roles = roles;

            IsAuthenticated = true;
            KeepSession();
        }

        public void Refresh(string id, string userName, string email, List<string> roles)
        {
            Id = id;
            UserName = userName;
            Email = email;
            Roles = roles;
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
