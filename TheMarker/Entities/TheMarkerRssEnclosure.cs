using System.Xml.Serialization;

namespace TheMarker.Entities
{
    [XmlRoot(ElementName="enclosure")]
    public class TheMarkerRssEnclosure
    {
        [XmlAttribute(AttributeName = "url")]
        public string Url { get; set; }
        
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
    }
}