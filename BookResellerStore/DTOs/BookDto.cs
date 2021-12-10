using System;
using System.Xml.Serialization;

namespace BookResellerStore.DTOs
{
    [Serializable()]
    [XmlRoot(ElementName = "book")]
    public class BookDto
    {
        public BookDto()
        {
            BookId = Guid.NewGuid();
        }

        [XmlElement("isbncode")]
        public string Isbncode { get; set; }

        [XmlElement("title")]
        public string BookName { get; set; }

        [XmlElement("author")]
        public string Author { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }

        public Guid BookId { get; set; }

        public string StoreName { get; set; }

        public Guid StoreId { get; set; }

        public int NumberInStock { get; set; }
    }
}
