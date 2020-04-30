using System.Xml.Serialization;

namespace Haaretz.Entities
{
    [XmlRoot(ElementName = "item")]
    public class HaaretzRssImage
    {
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "link")]
        public string Link { get; set; }

        [XmlElement(ElementName = "url")]
        public string Url { get; set; }
        
        [XmlElement(ElementName = "width")]
        public int Width { get; set; }
        
        [XmlElement(ElementName = "height")]
        public int Height { get; set; }
    }
}