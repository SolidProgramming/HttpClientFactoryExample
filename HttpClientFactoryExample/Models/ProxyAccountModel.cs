using System.Text.Json.Serialization;

namespace HttpClientFactoryExample.Models
{
    public class ProxyAccountModel
    {
        [JsonPropertyName("URI")]
        public string Uri { get; set; } = default!;

        [JsonPropertyName("Username")]
        public string Username { get; set; } = default!;

        [JsonPropertyName("Password")]
        public string Password { get; set; } = default!;
    }
}
