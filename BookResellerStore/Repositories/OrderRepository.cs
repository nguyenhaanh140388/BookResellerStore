using BookResellerStore.Common;
using BookResellerStore.DTOs;
using BookResellerStore.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static BookResellerStore.Common.Constants;

namespace BookResellerStore.Repositories
{
    public class OrderRepository : IOrderRepositoty
    {
        private readonly IHostingEnvironment hostingEnvironment;

        public OrderRepository(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        public Task<ResultDto> CreateOrder(OrderDto order)
        {
            ResultDto resultDto = new ResultDto()
            {
                Success = true,
                ReturnData = (int)OrderResult.OrderSuccessful
            }; ;

            if (order == null)
            {
                resultDto = new ResultDto()
                {
                    Success = false,
                    ReturnData = (int)OrderResult.OrderFail
                };

                return Task.Run(() =>
                {
                    return resultDto;
                });
            }

            if (order.Quantity == 0)
            {
                resultDto = new ResultDto()
                {
                    Success = false,
                    ReturnData = (int)OrderResult.QuantityInValid
                };
            }

            var currentOrder = GlobalConfiguration.Orders.Where(x => x.BookId == order.BookId).SingleOrDefault();
            var bookStores = GlobalConfiguration.BookStores;
            int numberInStock = (from s in bookStores
                                 from b in s.Books
                                 where b.BookId == order.BookId
                                 select b.NumberInStock).SingleOrDefault();

            if (currentOrder != null)
            {
                // int finalQuantity = currentOrder.Quantity + order.Quantity;
                if (order.Quantity > numberInStock)
                {
                    resultDto = new ResultDto()
                    {
                        Success = false,
                        ReturnData = (int)OrderResult.ExceedQuantityStock
                    };
                }
                else
                {
                    currentOrder.Quantity += order.Quantity;
                }
            }
            else
            {
                order.OrderId = Guid.NewGuid();
                GlobalConfiguration.Orders.Add(order);
            }

            if (resultDto.Success)
            {
                // Subtract the quantity in stock
                var bookInStock = (from s in bookStores
                                   join b in GlobalConfiguration.BookStores.SelectMany(x => x.Books)
                                   on s.StoreId equals b.StoreId
                                   where b.BookId == order.BookId
                                   select b).SingleOrDefault();
                bookInStock.NumberInStock -= order.Quantity;
            }

            return Task.Run(() =>
            {
                return resultDto;
            });
        }

        public Task<IEnumerable<OrderedBook>> GetOrders()
        {
            var allOrders = from o in GlobalConfiguration.Orders
                            join b in GlobalConfiguration.BookStores.SelectMany(x => x.Books)
                            on o.BookId equals b.BookId
                            join s in GlobalConfiguration.BookStores
                            on b.StoreId equals s.StoreId
                            select new OrderedBook
                            {
                                BookId = b.BookId,
                                BookName = b.BookName,
                                Isbncode = b.Isbncode,
                                Price = b.Price * o.Quantity,
                                StoreName = s.StoreName,
                                StoreId = s.StoreId,
                                Quanlity = o.Quantity
                            };

            return Task.Run(() =>
            {
                return allOrders;
            });
        }

        public Task<ResultDto> ExportOrders()
        {
            ResultDto resultDto = new ResultDto()
            {
                Success = true,
                ReturnData = (int)OrderExportResult.ExportSuccessful,
            };

            try
            {
                var exportedOrders = from o in GlobalConfiguration.Orders
                                     join b in GlobalConfiguration.BookStores.SelectMany(x => x.Books)
                                     on o.BookId equals b.BookId
                                     join s in GlobalConfiguration.BookStores
                                     on b.StoreId equals s.StoreId
                                     select new ExportedOrder
                                     {
                                         IsbnCode = b.Isbncode,
                                         StoreId = s.StoreId,
                                         ContactEmail = s.ContactEmail
                                     };

                if (exportedOrders == null)
                {
                    resultDto = new ResultDto()
                    {
                        Success = false,
                        ReturnData = (int)OrderExportResult.EmptyOrders,
                    };
                }

                if (exportedOrders.Count() == 0)
                {
                    resultDto = new ResultDto()
                    {
                        Success = false,
                        ReturnData = (int)OrderExportResult.NoOrders,
                    };
                }

                string outputFilePath = Path.Combine(hostingEnvironment.WebRootPath, "OrderFiles", string.Concat("OrderFile_", DateTime.Now.ToString("yyyyMMddHHmmssfff")));
                Utils.SerializeToXml<List<ExportedOrder>>(exportedOrders.ToList(), outputFilePath);
            }
            catch (Exception ex)
            {
                resultDto = new ResultDto()
                {
                    Success = false,
                    ReturnData = (int)OrderExportResult.ExportFail,
                    Error = ex.Message
                };
            }

            return Task.Run(() =>
            {
                return resultDto;
            });
        }
    }
}
