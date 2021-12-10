using BookResellerStore.Auth;
using BookResellerStore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookResellerStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly IStoreRepository storeRepository;

        public StoreController(IStoreRepository storeRepository)
        {
            this.storeRepository = storeRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetStores()
        {
            var result = await storeRepository.GetStores();
            return this.Ok(result);
        }
    }
}
