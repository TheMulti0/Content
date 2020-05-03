using System.Text.Json.Serialization;

namespace Galatz.Entities
{
    public class Program
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}