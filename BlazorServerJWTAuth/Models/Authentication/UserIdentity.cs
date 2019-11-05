using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerJWTAuth.Models.Authentication
{
    public class UserIdentity
    {
        public UserIdentity()
        {
            IsAuthenticated = false;
            Roles = new List<string>();
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsAuthenticated { get; set; }
        public List<string> Roles { get; set; }


    }
}
