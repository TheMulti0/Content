using System.Xml.Serialization;

namespace TheMarker.Entities
{
    [XmlRoot(ElementName="content")]
    public class TheMarkerRssContent
    {
        [XmlAttribute(AttributeName = "width")]
        public string Width { get; set; }
        
        [XmlAttribute(AttributeName = "url")]
        public string Url { get; set; }
    }
}