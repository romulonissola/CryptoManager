using Newtonsoft.Json;

namespace CryptoManager.Domain.IntegrationEntities.Facebook
{
    public class FacebookAppAccessToken
    {
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
