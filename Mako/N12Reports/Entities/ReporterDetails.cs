using System.Text.Json.Serialization;

namespace Mako.N12Reports.Entities
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