using System.Text.Json.Serialization;

namespace Models
{
    public class RedditTokenModel
    {
        [JsonPropertyName("access_token")]
        public string? AccessToken { get; set; }
        
        [JsonPropertyName("expires_in")]
        public long ExpiresIn { get; set; }
        
        [JsonPropertyName("Scope")]
        public string? Read { get; set; }

        [JsonPropertyName("token_type")]
        public string? TokenType { get; set; }
    }
}
