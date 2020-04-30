using System.Xml.Serialization;

namespace Walla.Entities
{
    [XmlRoot(ElementName="rss")]
    public class WallaRssFeed
    {
        [XmlElement(ElementName="channel")]
        public WallaRssChannel Channel { get; set; }
        
        [XmlAttribute(AttributeName="version")]
        public string Version { get; set; }
    }
}