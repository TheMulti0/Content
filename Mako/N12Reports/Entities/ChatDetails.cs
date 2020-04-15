using System.Text.Json.Serialization;

namespace Mako.N12Reports.Entities
{
    public class ChatDetails
    {
        [JsonPropertyName("chatID")]
        public int ChatId { get; set; }
        
        [JsonPropertyName("chatName")]
        public string Name { get; set; }
        
        [JsonPropertyName("socialID")]
        public int SocialId { get; set; }
        
        [JsonPropertyName("chatSocialID")]
        public string ChatSocialId { get; set; }
        
        [JsonPropertyName("status")]
        public int Status { get; set; }
    }
}