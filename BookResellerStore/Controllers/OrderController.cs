using BookResellerStore.Auth;
using BookResellerStore.Common;
using BookResellerStore.DTOs;
using BookResellerStore.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using static BookResellerStore.Common.Constants;

namespace BookResellerStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepositoty orderRepositoty;

        [Permission(RightName.ViewOrder)]
        public async Task<IActionResult> Index()
        {
            var result = await orderRepositoty.GetOrders();
            return PartialView(result);
        }

        public async Task<IActionResult> TotalOrder()
        {
            var result = await orderRepositoty.GetOrders();

            return this.Ok(result.Count());
        }

        public OrderController(IOrderRepositoty orderRepositoty)
        {
            this.orderRepositoty = orderRepositoty;

        }

        [Permission(RightName.CreateOrder)]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDto order)
        {
            var result = await orderRepositoty.CreateOrder(order);
            return this.Ok(result);
        }

        [Permission(RightName.ExportOrder)]
        public async Task<IActionResult> ExportOrder()
        {
            var result = await orderRepositoty.ExportOrders();
            return this.Ok(result);
        }
    }
}
