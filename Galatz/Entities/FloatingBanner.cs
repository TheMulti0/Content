using System.Text.Json.Serialization;

namespace Galatz.Entities
{
    public class FloatingBanner
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("subTitle")]
        public object SubTitle { get; set; }

        [JsonPropertyName("img")]
        public string Img { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("target")]
        public object Target { get; set; }
    }
}