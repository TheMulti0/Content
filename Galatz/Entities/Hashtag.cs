using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Galatz.Entities
{
    public class Hashtag
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("img")]
        public string Img { get; set; }

        [JsonPropertyName("talents")]
        public List<Talent> Talents { get; set; }

        [JsonPropertyName("program")]
        public Program Program { get; set; }

        [JsonPropertyName("metaTitle")]
        public string MetaTitle { get; set; }

        [JsonPropertyName("duration")]
        public string Duration { get; set; }

        [JsonPropertyName("title")]
        public Title Title { get; set; }

        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("hashtag")]
        public string _Hashtag { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("isNewsPage")]
        public bool IsNewsPage { get; set; }
    }
}