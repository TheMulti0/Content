using System.Xml.Serialization;

namespace N0404.Entities
{
    [XmlRoot(ElementName="enclosure")]
    public class N0404RssEnclosure
    {
        [XmlAttribute("url")]
        public string Url { get; set; }
        
        [XmlAttribute("type")]
        public string Type { get; set; }
    }
}