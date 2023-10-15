using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace TennisWeb.Auth
{
    public class CustomPrincipal : IPrincipal
    {
        public CustomPrincipal(string email, string role)
        {
            Identity = new GenericIdentity(email);
            Role = role;
        }

        public IIdentity Identity { get; private set; }
        public string Role { get; private set; }

        public bool IsInRole(string role)
        {
            return Role.Equals(role, StringComparison.OrdinalIgnoreCase);
        }
    }

}