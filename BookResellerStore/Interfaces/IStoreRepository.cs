using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookResellerStore.Interfaces
{
    public interface IStoreRepository
    {
        Task<IEnumerable<string>> GetStores();
    }
}
