using System.Text.Json.Serialization;

namespace Calabonga.AspNetCore.IdentityDebug
{
    /// <summary>
    /// Authentication key=value pairs for DEBUG 
    /// </summary>
    public class IdentityData
    {
        [JsonPropertyName("key")]
        public string Key { get; set; } = null!;

        [JsonPropertyName("value")]
        public string Value { get; set; } = null!;
    }
}
