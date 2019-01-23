using System.Collections.Generic;

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



}
