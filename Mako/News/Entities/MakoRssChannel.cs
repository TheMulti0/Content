using System.Collections.Generic;
using System.Xml.Serialization;

namespace Mako.News.Entities
{
    [XmlRoot(ElementName="channel")]
    public class MakoRssChannel 
    {
        [XmlElement(ElementName="title")]
        public string Title { get; set; }
        
        [XmlElement(ElementName="link")]
        public string Link { get; set; }
        
        [XmlElement(ElementName="description")]
        public string Description { get; set; }
        
        [XmlElement(ElementName="language")]
        public string Language { get; set; }
        
        [XmlElement(ElementName="item")]
        public List<MakoRssItem> Items { get; set; }
    }
}