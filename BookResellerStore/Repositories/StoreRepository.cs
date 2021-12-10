using BookResellerStore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BookResellerStore.Common.Constants;

namespace BookResellerStore.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        public Task<IEnumerable<string>> GetStores()
        {
            return Task.Run(() =>
            {
                return Enum.GetNames(typeof(BookStore)).AsEnumerable();
            });
        }
    }
}
