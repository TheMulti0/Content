using System.Text.Json.Serialization;

namespace Galatz.Entities
{
    public class SectionTitles
    {
        [JsonPropertyName("talentsTitle")]
        public FloatingBanner TalentsTitle { get; set; }

        [JsonPropertyName("podcastsTitle")]
        public FloatingBanner PodcastsTitle { get; set; }

        [JsonPropertyName("universityCardTitle")]
        public FloatingBanner UniversityCardTitle { get; set; }

        [JsonPropertyName("archiveItems")]
        public FloatingBanner ArchiveItems { get; set; }
    }
}