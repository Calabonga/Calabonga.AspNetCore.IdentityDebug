namespace Calabonga.AspNetCore.IdentityDebug
{
    /// <summary>
    /// Options for authentication for DEBUG
    /// </summary>
    public sealed class DebugAuthenticationOptions
    {
        public DebugAuthenticationOptions(string jsonPath)
        {
            JsonPath = jsonPath;
        }
        public string JsonPath { get; }
    }
}
