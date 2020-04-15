using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mako.N12Reports.Entities
{
    public class Report
    {
        [JsonPropertyName("messageID")]
        public int MessageId { get; set; }
        
        [JsonPropertyName("messageSocialD")]
        public string MessageSocialId { get; set; }
        
        [JsonPropertyName("reporter")]
        public Reporter Reporter { get; set; }
        
        [JsonPropertyName("chat")]
        public ChatDetails Chat { get; set; }
        
        [JsonPropertyName("firebaseID")]
        public string FirebaseId { get; set; }
        
        [JsonPropertyName("statusID")]
        public int StatusId { get; set; }
        
        [JsonPropertyName("messageContent")]
        public string Content { get; set; }
        
        [JsonPropertyName("createdDate")]
        public long CreatedDate { get; set; }
        
        [JsonPropertyName("databaseTimestamp")]
        public long DatabaseTimestamp { get; set; }
        
        [JsonPropertyName("updatedDate")]
        public long UpdatedDate { get; set; }
        
        [JsonPropertyName("publishedDate")]
        public long PublishedDate { get; set; }
        
        [JsonPropertyName("updatePublishTime")]
        public bool UpdatePublishTime { get; set; }
        
        [JsonPropertyName("medias")]
        public List<MediaInfo> MediaInfos { get; set; }
        
        [JsonPropertyName("anUpdate")]
        public bool IsUpdated { get; set; }
        
        [JsonPropertyName("important")]
        public bool IsImportant { get; set; }
    }
}