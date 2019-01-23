using System.Collections.Generic;
using System.Threading.Tasks;

namespace Altkom.DotnetCore.IServices
{
    public interface IItemService<TItem>
    {
        IEnumerable<TItem> Get();
        TItem Get(int id);
        void Add(TItem item);
        void Remove(int id);
        void Update(TItem item);
    }

    public interface IItemServiceAsync<TItem>
    {
        Task<IEnumerable<TItem>> GetAsync();
        Task<TItem> GetAsync(int id);
        Task AddAsync(TItem item);
        Task RemoveAsync(int id);
        Task UpdateAsync(TItem item);
    }



}
