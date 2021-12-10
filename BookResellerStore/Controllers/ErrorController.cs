using BookResellerStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
