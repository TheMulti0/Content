using System.Collections.Generic;
using System.Xml.Serialization;

namespace Calcalist.Entities
{
    [XmlRoot(ElementName="channel")]
    public class CalcalistRssChannel
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
        public CalcalistRssImage Image { get; set; }

        [XmlElement(ElementName="item")]
        public List<CalcalistRssItem> Items { get; set; }
    }
}