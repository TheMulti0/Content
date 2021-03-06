﻿using System.Collections.Generic;
using System.Xml.Serialization;

namespace Haaretz.Entities
{
    [XmlRoot(ElementName="channel")]
    public class HaaretzRssChannel
    {
        [XmlElement(ElementName="title")]
        public string Title { get; set; }
        
        [XmlElement(ElementName="link")]
        public string Link { get; set; }
        
        [XmlElement(ElementName="lastBuildDate")]
        public string LastBuildDate { get; set; }
        
        [XmlElement(ElementName="language")]
        public string Language { get; set; }
        
        [XmlElement(ElementName="item")]
        public List<HaaretzRssItem> Items { get; set; }
    }
}