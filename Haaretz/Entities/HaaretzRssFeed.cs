using System.Xml.Serialization;

namespace Haaretz.Entities
{
    [XmlRoot(ElementName="rss")]
    public class HaaretzRssFeed
    {
        [XmlElement(ElementName="channel")]
        public HaaretzRssChannel Channel { get; set; }
        
        [XmlAttribute(AttributeName="version")]
        public string Version { get; set; }
    }
}