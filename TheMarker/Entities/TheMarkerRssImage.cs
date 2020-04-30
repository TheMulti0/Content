using System.Xml.Serialization;

namespace TheMarker.Entities
{
    [XmlRoot(ElementName = "item")]
    public class TheMarkerRssImage
    {
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "link")]
        public string Link { get; set; }

        [XmlElement(ElementName = "url")]
        public string Url { get; set; }
    }
}