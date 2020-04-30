using System.Xml.Serialization;

namespace Walla.Entities
{
    [XmlRoot(ElementName="item")]
    public class WallaRssItem
    {
        [XmlElement(ElementName="title")]
        public string Title { get; set; }
        
        [XmlElement(ElementName="link")]
        public string Link { get; set; }

        [XmlElement(ElementName="description")]
        public string Description { get; set; }
        
        [XmlElement(ElementName="pubDate")]
        public string PublishDate { get; set; }
    }
}