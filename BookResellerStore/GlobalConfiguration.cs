using BookResellerStore.Common;
using BookResellerStore.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookResellerStore
{
    public static class GlobalConfiguration
    {
        static GlobalConfiguration()
        {
            BookStores = new List<StoreDto>();
            Orders = new List<OrderDto>();
        }

        public static List<StoreDto> BookStores { get; private set; }
        public static List<OrderDto> Orders { get; private set; }

        public static void InitData(string xmlPath)
        {
            if (Directory.Exists(xmlPath))
            {
                DirectoryInfo dirSource = new DirectoryInfo(xmlPath);
                var allXMLFiles = dirSource.GetFiles("*.xml", SearchOption.AllDirectories).ToList();

                foreach (var nextFile in allXMLFiles)
                {
                    try
                    {
                        StoreDto result = Utils.DeserializeToObject<StoreDto>(nextFile.FullName);
                        result.StoreId = Guid.NewGuid();
                        
                        for (int i = 0; i < result.Books.Count; i++)
                        {
                            result.Books[i].NumberInStock = 100;
                            result.Books[i].StoreId = result.StoreId;
                        }

                        BookStores.Add(result);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
    }
}
