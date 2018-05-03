using IdentityServer4.Models;
using System.Collections.Generic;

namespace PiensaEnColores.IdentityServer
{
  public class Config
  {
    public static IEnumerable<ApiResource> GetApiResources()
    {
      return new List<ApiResource>
    {
        new ApiResource("APICustomer", "API de los customers de ENCAMINA"),
        new ApiResource("APIEmployee", "API de los empleados de ENCAMINA")
    };
    }

    public static IEnumerable<Client> GetClients()
    {
      return new List<Client>
    {
        new Client
        {
            ClientId = "MyEncamina",
            // no interactive user, use the clientid/secret for authentication
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            // secret for authentication
            ClientSecrets =
            {
                new Secret("piensaencolres".Sha256())
            },
            // scopes that client has access to
            AllowedScopes = { "APIEmployee", "APICustomer" }
        }
    };
    }
  }
}
