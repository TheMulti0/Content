using System.Text.Json.Serialization;

namespace Galatz.Entities
{
    public class ArchiveItem
    {
        [JsonPropertyName("info")]
        public string Info { get; set; }

        [JsonPropertyName("img")]
        public string Img { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("track")]
        public Track Track { get; set; }

        [JsonPropertyName("tag")]
        public string Tag { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}