using System.Collections.Generic;
using System.Xml.Serialization;

namespace N0404.Entities
{
    [XmlRoot(ElementName="item")]
    public class N0404RssItem
    {
        [XmlElement(ElementName="title")]
        public string Title { get; set; }

        [XmlElement(ElementName="link")]
        public string Link { get; set; }

        [XmlElement(ElementName="pubDate")]
        public string PublishDate { get; set; }

        [XmlElement(ElementName = "creator", Namespace="http://purl.org/dc/elements/1.1/")]
        public string Creator { get; set; }

        [XmlElement(ElementName = "identifier", Namespace="http://purl.org/dc/elements/1.1/")]
        public string Identifier { get; set; }

        [XmlElement(ElementName = "modified", Namespace="http://purl.org/dc/elements/1.1/")]
        public string Modified { get; set; }

        [XmlElement(ElementName = "created", Namespace="http://purl.org/dc/elements/1.1/")]
        public string Created { get; set; }

        [XmlElement(ElementName="guid")]
        public string Guid { get; set; }

        [XmlElement(ElementName="category")]
        public List<int> Categories { get; set; }
        
        [XmlElement(ElementName="description")]
        public string Description { get; set; }

        [XmlElement(ElementName="encoded", Namespace="http://purl.org/rss/1.0/modules/content/")]
        public string Encoded { get; set; }
        
        [XmlElement(ElementName="enclosure")]
        public N0404RssEnclosure Enclosure { get; set; }
    }
}