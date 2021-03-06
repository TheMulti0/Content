﻿using System.Xml.Serialization;

namespace Ynet.Entities
{
    [XmlRoot(ElementName="rss")]
    public class YnetRssFeed 
    {
        [XmlElement(ElementName="channel")]
        public YnetRssChannel Channel { get; set; }
        
        [XmlAttribute(AttributeName="version")]
        public string Version { get; set; }
    }
}