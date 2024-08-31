using System.Text.Json.Serialization;

namespace HttpClientFactoryExample.Models
{
    public class SettingsModel
    {       
        [JsonPropertyName("Proxy")]
        public ProxyAccountModel ProxySettings { get; set; } = default!;

        //[JsonPropertyName(name: "AppSettings")]
        //public AppSettingsModel AppSettings { get; set; } = default!;        
    }
}
