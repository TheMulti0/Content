using System.Xml.Serialization;

namespace N0404.Entities
{
    [XmlRoot(ElementName = "item")]
    public class N0404RssImage
    {
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "link")]
        public string Link { get; set; }

        [XmlElement(ElementName = "url")]
        public string Url { get; set; }
    }
}