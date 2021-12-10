using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BookResellerStore.DTOs
{
    [Serializable()]
    [System.Xml.Serialization.XmlRoot("bookstore")]
    public class StoreDto
    {
        public Guid StoreId { get; set; }

        [XmlAttribute("name")]
        public string StoreName { get; set; }

        [XmlAttribute("contactemail")]
        public string ContactEmail { get; set; }

        [XmlElement(ElementName = "book")]
        public List<BookDto> Books { get; set; }
    }
}
