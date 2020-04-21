using System.Xml.Serialization;

namespace Ynet.Entities
{
    [XmlRoot(ElementName="item")]
    public class YnetRssItem 
    {
        [XmlElement(ElementName = "category")]
        public string Category { get; set; }
        
        [XmlElement(ElementName="title")]
        public string Title { get; set; }
        
        [XmlElement(ElementName="description")]
        public string Description { get; set; }
        
        [XmlElement(ElementName="link")]
        public string Link { get; set; }
        
        [XmlElement(ElementName="pubDate")]
        public string PublishDate { get; set; }
        
        [XmlElement(ElementName="guid")]
        public string Guid { get; set; }
        
        [XmlElement(ElementName="tags")]
        public string Tags { get; set; }
    }
}