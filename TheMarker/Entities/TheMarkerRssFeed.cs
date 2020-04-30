using System.Xml.Serialization;

namespace TheMarker.Entities
{
    [XmlRoot(ElementName="rss")]
    public class TheMarkerRssFeed
    {
        [XmlElement(ElementName="channel")]
        public TheMarkerRssChannel Channel { get; set; }
        
        [XmlAttribute(AttributeName="version")]
        public string Version { get; set; }
    }
}