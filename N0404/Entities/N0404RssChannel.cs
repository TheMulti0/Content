using System.Collections.Generic;
using System.Xml.Serialization;

namespace N0404.Entities
{
    [XmlRoot(ElementName="channel")]
    public class N0404RssChannel
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
        public N0404RssImage Image { get; set; }

        [XmlElement(ElementName="item")]
        public List<N0404RssItem> Items { get; set; }
    }
}