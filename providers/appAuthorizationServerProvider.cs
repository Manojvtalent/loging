using Microsoft.Owin.Security.OAuth;
using System.Threading.Tasks;

namespace WebApplication.providers
{
    public class appAuthorizationServerProvider:OAuthAuthorizationServerProvider
    {
        public async override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            return base.GrantResourceOwnerCredentials(context);
        }
    }
}
