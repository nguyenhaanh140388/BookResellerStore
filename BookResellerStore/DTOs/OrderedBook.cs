using System;

namespace BookResellerStore.DTOs
{
    public class OrderedBook
    {
        public string Isbncode { get; set; }
        public string BookName { get; set; }
        public int Quanlity { get; set; }
        public decimal Price { get; set; }
        public string StoreName { get; set; }
        public string ContactEmail { get; set; }
        public Guid BookId { get; set; }
        public Guid StoreId { get; set; }
    }
}
