using System.Text.Json.Serialization;

namespace Galatz.Entities
{
    public class Seo
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("name")]
        public object Name { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("url")]
        public object Url { get; set; }

        [JsonPropertyName("folderName")]
        public string FolderName { get; set; }
    }
}