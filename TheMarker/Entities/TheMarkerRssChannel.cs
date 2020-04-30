using System.Collections.Generic;
using System.Xml.Serialization;

namespace TheMarker.Entities
{
    [XmlRoot(ElementName="channel")]
    public class TheMarkerRssChannel
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
        public TheMarkerRssImage Image { get; set; }

        [XmlElement(ElementName="item")]
        public List<TheMarkerRssItem> Items { get; set; }
    }
}