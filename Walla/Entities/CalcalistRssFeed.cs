using System.Xml.Serialization;

namespace Walla.Entities
{
    [XmlRoot(ElementName="rss")]
    public class CalcalistRssFeed
    {
        [XmlElement(ElementName="channel")]
        public CalcalistRssChannel Channel { get; set; }
        
        [XmlAttribute(AttributeName="version")]
        public string Version { get; set; }
    }
}