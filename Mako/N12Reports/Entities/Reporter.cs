using System.Text.Json.Serialization;

namespace Mako.N12Reports.Entities
{
    public class Reporter
    {
        [JsonPropertyName("nickname")]
        public string Nickname { get; set; }
        
        [JsonPropertyName("status")]
        public int Status { get; set; }
        
        [JsonPropertyName("reporter")]
        public ReporterDetails Details { get; set; }
        
        [JsonPropertyName("socialNetwork")]
        public SocialNetwork SocialNetwork { get; set; }
        
        [JsonPropertyName("rsgid")]
        public int RsgId { get; set; }
    }
}