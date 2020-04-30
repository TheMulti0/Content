using System.Xml.Serialization;

namespace Haaretz.Entities
{
    [XmlRoot(ElementName="enclosure")]
    public class HaaretzRssEnclosure
    {
        [XmlAttribute("url")]
        public string Url { get; set; }
        
        [XmlAttribute("type")]
        public string Type { get; set; }
    }
}