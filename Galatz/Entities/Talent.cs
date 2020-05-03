using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Galatz.Entities
{
    public class Talent
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("img")]
        public string Img { get; set; }

        [JsonPropertyName("programs")]
        public List<Program> Programs { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}