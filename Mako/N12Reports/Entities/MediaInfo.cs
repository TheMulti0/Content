using System.Text.Json.Serialization;

namespace Mako.N12Reports.Entities
{
    public class MediaInfo
    {
        [JsonPropertyName("autoId")]
        public int AutoId { get; set; }
        
        [JsonPropertyName("link1")]
        public string LowResolutionImage { get; set; }
        
        [JsonPropertyName("link2")]
        public string MediumResolutionImage { get; set; }
        
        [JsonPropertyName("link3")]
        public string HighResolutionImage { get; set; }
        
        [JsonPropertyName("typeId")]
        public int TypeId { get; set; }
        
        [JsonPropertyName("linkOrder")]
        public int LinkOrder { get; set; }
        
        [JsonPropertyName("legthInSeconds")]
        public int Length { get; set; }
        
        [JsonPropertyName("mediaContent")]
        public string Description { get; set; }
        
        [JsonPropertyName("messageId")]
        public int MessageId { get; set; }
    }
}