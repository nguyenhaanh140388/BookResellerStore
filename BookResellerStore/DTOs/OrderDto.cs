using System;

namespace BookResellerStore.DTOs
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public int Quantity { get; set; }
        public Guid StoreId { get; set; }
        public Guid BookId { get; set; }
    }
}
