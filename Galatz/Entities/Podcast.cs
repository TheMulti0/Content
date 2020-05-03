using System.Text.Json.Serialization;

namespace Galatz.Entities
{
    public class Podcast
    {
        [JsonPropertyName("name")]
        public Track Name { get; set; }

        [JsonPropertyName("info")]
        public string Info { get; set; }

        [JsonPropertyName("img")]
        public string Img { get; set; }

        [JsonPropertyName("chapter")]
        public Track Chapter { get; set; }

        [JsonPropertyName("track")]
        public Track Track { get; set; }
    }
}