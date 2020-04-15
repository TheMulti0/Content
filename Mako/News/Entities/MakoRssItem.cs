using System.Xml.Serialization;

namespace Mako.News.Entities
{
    [XmlRoot(ElementName="item")]
    public class MakoRssItem 
    {
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
        
        [XmlElement(ElementName="shortDescription")]
        public string ShortDescription { get; set; }
        
        [XmlElement(ElementName="image")]
        public string Thumbnail { get; set; }
        
        [XmlElement(ElementName="image139X80")]
        public string LowResImage { get; set; }
        
        [XmlElement(ElementName="image435X329")]
        public string MediumResImage { get; set; }
        
        [XmlElement(ElementName="image624X383")]
        public string HighResImage { get; set; }
        
        [XmlElement(ElementName="photographer")]
        public string Photographer { get; set; }
    }
}