using System.Xml.Serialization;

namespace Mako.News.Entities
{
    [XmlRoot(ElementName="rss")]
    public class MakoRssFeed 
    {
        [XmlElement(ElementName="channel")]
        public MakoRssChannel Channel { get; set; }
        
        [XmlAttribute(AttributeName="version")]
        public string Version { get; set; }
    }
}