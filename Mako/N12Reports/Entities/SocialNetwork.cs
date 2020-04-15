using System.Text.Json.Serialization;

namespace Mako.N12Reports.Entities
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