using System.Text.Json.Serialization;

namespace Mako.Reporters.Entities
{
    public class SocialNetwork
    {
        [JsonPropertyName("socialID")]
        public int Id { get; set; }
        
        [JsonPropertyName("socialName")]
        public string Name { get; set; }
        
        [JsonPropertyName("status")]
        public int Status { get; set; }
    }
}