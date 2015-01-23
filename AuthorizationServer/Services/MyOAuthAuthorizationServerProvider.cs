using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Caching;
using AuthorizationServer.Models;
using Microsoft.Owin.Security.OAuth;

namespace AuthorizationServer.Services
{
    public class MyOAuthAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId, clientSecret;

            if (context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                var client =
                    DataStore.Store.Clients.FirstOrDefault(s => s.Id == clientId && s.ClientSecretHash == clientSecret);
                if (client != null)
                {
                    context.OwinContext.Set<Client>("oauth:client", client);
                    context.Validated();
                }
                else
                {
                    context.SetError("invalid_client", "Invalid credidentials");
                    context.Rejected();
                }
            }

            return Task.FromResult(0);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var client = context.OwinContext.Get<Client>("oauth:client");
            if (client.AllowedGrant == OAuthGrant.ResourceOwner)
            {
                var claimsIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
                //claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, context.ClientId));
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                context.Validated(claimsIdentity);
            }
            else
            {
                context.SetError("invalid_grant", "Client is not allowed for the 'Resource Owner Password Credentials Grant'");
            }

            return Task.FromResult(0);
        }
    }
}