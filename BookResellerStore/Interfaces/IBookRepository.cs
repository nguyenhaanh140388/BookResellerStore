using BookResellerStore.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookResellerStore.Interfaces
{
    public interface IBookRepository
    {
        Task<BookDto> FindById(Guid bookId);
        Task<IEnumerable<BookDto>> GetListAvailableBooks(string storeName);
        Task<IEnumerable<BookDto>> FindByKeyword(string keyWord);
    }
}
