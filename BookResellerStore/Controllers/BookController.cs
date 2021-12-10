using BookResellerStore.Auth;
using BookResellerStore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookResellerStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public async Task<IActionResult> FindByKeyword(string keyWord)
        {
            string keyWordPara = this.Request.Query["keyWord"];
            var result = await bookRepository.FindByKeyword(keyWordPara);
            return this.Ok(result);
        }

        public async Task<IActionResult> BookDetail(Guid bookId)
        {
            var result = await bookRepository.FindById(bookId);
            return PartialView(result);
        }

        public async Task<IActionResult> GetListAvailableBooks(string storeName)
        {
            string storeNamePara = this.Request.Query["storeName"];
            var result = await bookRepository.GetListAvailableBooks(storeNamePara);
            return this.Ok(result);
        }
    }
}
