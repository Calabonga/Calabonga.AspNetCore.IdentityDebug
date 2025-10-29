using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Calabonga.AspNetCore.IdentityDebug
{
    /// <summary>
    /// Authentication faked identity dictionary for DEBUG
    /// </summary>
    public class DebugIdentity
    {
        [JsonPropertyName("items")]
        public List<IdentityData> Items { get; set; } = new List<IdentityData>();
    }
}
