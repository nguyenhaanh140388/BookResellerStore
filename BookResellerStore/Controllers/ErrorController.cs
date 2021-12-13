using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookResellerStore.Controllers
{
    public class ErrorController : Controller
    {
        [AllowAnonymous]
        public IActionResult NoPermission()
        {
            return PartialView();
        }
    }
}
