using BookResellerStore.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookResellerStore.Interfaces
{
    public interface IOrderRepositoty
    {
        Task<IEnumerable<OrderedBook>> GetOrders();
        Task<ResultDto> CreateOrder(OrderDto orderDto);
        Task<ResultDto> ExportOrders();
    }
}
