using System.Collections.Generic;
using System.Xml.Serialization;

namespace TheMarker.Entities
{
    [XmlRoot(ElementName="item")]
    public class TheMarkerRssItem
    {
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "link")]
        public string Link { get; set; }

        [XmlElement(ElementName = "description")]
        public string Description { get; set; }

        [XmlElement(ElementName = "guid")]
        public string Guid { get; set; }

        [XmlElement(ElementName = "pubDate")]
        public string PublishDate { get; set; }

        [XmlElement(ElementName = "author")]
        public string Author { get; set; }

        [XmlElement(ElementName = "category")]
        public List<string> Categories { get; set; }

        [XmlElement(ElementName = "enclosure")]
        public TheMarkerRssEnclosure Enclosure { get; set; }

        [XmlElement(ElementName = "content", Namespace = "http://search.yahoo.com/mrss/")]
        public TheMarkerRssContent Content { get; set; }
    }
}