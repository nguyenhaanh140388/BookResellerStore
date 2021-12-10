using BookResellerStore.DTOs;
using BookResellerStore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookResellerStore.Repositories
{
    public class BookRepository : IBookRepository
    {
        public Task<BookDto> FindById(Guid bookId)
        {
            var bookStores = GlobalConfiguration.BookStores;
            var bookByStore = from s in bookStores
                              from b in s.Books
                              where b.BookId == bookId
                              select new BookDto
                              {
                                  Author = b.Author,
                                  BookName = b.BookName,
                                  Isbncode = b.Isbncode,
                                  Price = b.Price,
                                  NumberInStock = b.NumberInStock,
                                  StoreId = s.StoreId,
                                  StoreName = s.StoreName,
                                  BookId = b.BookId
                              };

            return Task.Run(() =>
            {
                return bookByStore.SingleOrDefault();
            });
        }

        public Task<IEnumerable<BookDto>> FindByKeyword(string keyWord)
        {
            var bookStores = GlobalConfiguration.BookStores;
            var bookByStore = from s in bookStores
                              from b in s.Books
                              where b.Author.Contains(keyWord)
                              || b.BookName.Contains(keyWord)
                              || b.Isbncode.Contains(keyWord)
                              || s.StoreName.Contains(keyWord)
                              select new BookDto
                              {
                                  Author = b.Author,
                                  BookName = b.BookName,
                                  Isbncode = b.Isbncode,
                                  Price = b.Price,
                                  NumberInStock = b.NumberInStock,
                                  StoreId = s.StoreId,
                                  StoreName = s.StoreName,
                                  BookId = b.BookId
                              };

            return Task.Run(() =>
            {
                return bookByStore;
            });
        }

        public Task<IEnumerable<BookDto>> GetListAvailableBooks(string storeName)
        {
            var bookStores = GlobalConfiguration.BookStores;
            var bookByStore = from s in bookStores
                              where s.StoreName == storeName
                              from b in s.Books
                              select new BookDto
                              {
                                  Author = b.Author,
                                  BookName = b.BookName,
                                  Isbncode = b.Isbncode,
                                  Price = b.Price,
                                  NumberInStock = b.NumberInStock,
                                  StoreId = s.StoreId,
                                  BookId = b.BookId
                              };

            return Task.Run(() =>
            {
                return bookByStore;
            });
        }
    }
}
