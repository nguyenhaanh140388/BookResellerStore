using System;

namespace BookResellerStore.DTOs
{
    public class ExportedOrder
    {
        public string IsbnCode { get; set; }
        public Guid StoreId { get; set; }
        public string ContactEmail { get; set; }
    }
}
