using System.Xml.Serialization;

namespace Haaretz.Entities
{
    [XmlRoot(ElementName="item")]
    public class HaaretzRssItem
    {
        [XmlElement(ElementName="title")]
        public string Title { get; set; }
        
        [XmlElement(ElementName="link")]
        public string Link { get; set; }

        [XmlElement(ElementName="description")]
        public string Description { get; set; }

        [XmlElement(ElementName = "guid")]
        public double Guid { get; set; }
        
        [XmlElement(ElementName="pubDate")]
        public string PublishDate { get; set; }

        [XmlElement("enclosure")]
        public HaaretzRssEnclosure Enclosure { get; set; }
    }
}