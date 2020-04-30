using System.Collections.Generic;
using System.Xml.Serialization;

namespace N0404.Entities
{
    [XmlRoot(ElementName="channel")]
    public class N0404RssChannel
    {
        [XmlElement(ElementName="title")]
        public string Title { get; set; }
        
        [XmlElement(ElementName="description")]
        public string Description { get; set; }
        
        [XmlElement(ElementName="link")]
        public string Link { get; set; }
        
        [XmlElement(ElementName="lastBuildDate")]
        public string LastBuildDate { get; set; }

        [XmlElement(ElementName="item")]
        public List<N0404RssItem> Items { get; set; }
    }
}