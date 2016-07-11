using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace InColUn.Auth
{
    public class OAuthStrategyOptions
    {
        public string CallbackPath { get; set; }

        public string AuthorizationEndpoint { get; set; }
        public string TokenEndpoint { get; set; }
        public string UserInformationEndpoint { get; set; }
        
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        public ICollection<string> Scope { get; } = new HashSet<string>();

        public Func<HttpContext, ClaimsIdentity, string, Task> OnOAuthSuccess { get; set; }
        public Func<HttpContext, string, string, Task> OnOAuthFailure { get; set; }

        public virtual string FormatScope()
        {
            return string.Join(" ", this.Scope);
        }
    }
}
