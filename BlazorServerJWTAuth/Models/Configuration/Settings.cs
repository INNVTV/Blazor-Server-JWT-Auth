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
        }

        public string IdentityServiceUri { get; set; }

    }
}
