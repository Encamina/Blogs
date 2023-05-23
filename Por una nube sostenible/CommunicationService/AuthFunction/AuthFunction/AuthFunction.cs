using Azure;
using Azure.Communication;
using Azure.Communication.Identity;
using Azure.Core;
using Microsoft.Azure.Functions.Worker;
using System.Text.Json;

namespace AuthFunction
{
    public class AuthFunction
    {

        [Function("function")]
        public static async Task<object> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequestMessage req)
        {
            string connectionString = "{YOUR_CONNECTION_STRING}";
            CommunicationIdentityClient identityClient = new CommunicationIdentityClient(connectionString);
            Response<CommunicationUserIdentifier> user = await identityClient.CreateUserAsync();
            Response<AccessToken> userToken = await identityClient.GetTokenAsync(user, new[] { CommunicationTokenScope.VoIP });

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            return JsonSerializer.Serialize(userToken.Value, options);
        }
    }
}
