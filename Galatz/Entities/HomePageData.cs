using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Galatz.Entities
{
    public class HomePageData
    {
        [JsonPropertyName("hashtags")]
        public List<Hashtag> Hashtags { get; set; }

        [JsonPropertyName("talents")]
        public List<Talent> Talents { get; set; }

        [JsonPropertyName("podcasts")]
        public List<Podcast> Podcasts { get; set; }

        [JsonPropertyName("universityCards")]
        public List<ArchiveItem> UniversityCards { get; set; }

        [JsonPropertyName("archiveItems")]
        public List<ArchiveItem> ArchiveItems { get; set; }

        [JsonPropertyName("floatingBanner")]
        public FloatingBanner FloatingBanner { get; set; }

        [JsonPropertyName("sectionTitles")]
        public SectionTitles SectionTitles { get; set; }

        [JsonPropertyName("seo")]
        public Seo Seo { get; set; }

        [JsonPropertyName("imgTweets")]
        public string ImgTweets { get; set; }
    }
}