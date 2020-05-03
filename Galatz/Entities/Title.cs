using System.Text.Json.Serialization;

namespace Galatz.Entities
{
    public class Title
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}