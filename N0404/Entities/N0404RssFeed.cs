using System.Xml.Serialization;

namespace N0404.Entities
{
    [XmlRoot(ElementName="rss")]
    public class N0404RssFeed
    {
        [XmlElement(ElementName="channel")]
        public N0404RssChannel Channel { get; set; }
        
        [XmlAttribute(AttributeName="version")]
        public string Version { get; set; }
    }
}