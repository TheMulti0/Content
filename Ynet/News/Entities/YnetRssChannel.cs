using System.Collections.Generic;
using System.Xml.Serialization;

namespace Ynet.News.Entities
{
    [XmlRoot(ElementName="channel")]
    public class YnetRssChannel 
    {
        [XmlElement(ElementName="title")]
        public string Title { get; set; }
        
        [XmlElement(ElementName="link")]
        public string Link { get; set; }
        
        [XmlElement(ElementName="description")]
        public string Description { get; set; }
        
        [XmlElement(ElementName="language")]
        public string Language { get; set; }
        
        [XmlElement(ElementName="pubDate")]
        public string PublishDate { get; set; }
        
        [XmlElement(ElementName="lastBuildDate")]
        public string LastBuildDate { get; set; }
        
        [XmlElement(ElementName="item")]
        public List<YnetRssItem> Items { get; set; }
    }
}