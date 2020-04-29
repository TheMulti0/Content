using System.Xml.Serialization;

namespace Calcaist.Entities
{
    [XmlRoot(ElementName="rss")]
    public class CalcaistRssFeed
    {
        [XmlElement(ElementName="channel")]
        public CalcaistRssChannel Channel { get; set; }
        
        [XmlAttribute(AttributeName="version")]
        public string Version { get; set; }
    }
}