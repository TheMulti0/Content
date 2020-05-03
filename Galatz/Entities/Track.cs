using System.Text.Json.Serialization;

namespace Galatz.Entities
{
    public class Track
    {
        [JsonPropertyName("trackId")]
        public long? TrackId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("target")]
        public string Target { get; set; }
    }
}