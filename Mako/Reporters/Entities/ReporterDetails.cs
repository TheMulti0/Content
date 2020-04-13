using System.Text.Json.Serialization;

namespace Mako.Reporters.Entities
{
    public class ReporterDetails
    {
        [JsonPropertyName("reporterId")]
        public int Id { get; set; }
        
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("image")]
        public string Image { get; set; }
        
        [JsonPropertyName("status")]
        public int Status { get; set; }
    }
}