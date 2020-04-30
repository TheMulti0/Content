using System.Collections.Generic;
using System.Xml.Serialization;

namespace Walla.Entities
{
    [XmlRoot(ElementName="channel")]
    public class WallaRssChannel
    {
        [XmlElement(ElementName="title")]
        public string Title { get; set; }
        
        [XmlElement(ElementName="link")]
        public string Link { get; set; }
        
        [XmlElement(ElementName="copyright")]
        public string Copyright { get; set; }
        
        [XmlElement(ElementName="language")]
        public string Language { get; set; }
        
        [XmlElement(ElementName="pubDate")]
        public string PublishDate { get; set; }
        
        [XmlElement(ElementName="lastBuildDate")]
        public string LastBuildDate { get; set; }

        [XmlElement(ElementName = "image")]
        public WallaRssImage Image { get; set; }

        [XmlElement(ElementName="item")]
        public List<WallaRssItem> Items { get; set; }
    }
}