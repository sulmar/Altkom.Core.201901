using Altkom.DotnetCore.IServices;
using Altkom.DotnetCore.Models;
using Bogus;
using System.Collections.Generic;
using System.Linq;

namespace Altkom.DotnetCore.FakeServices
{
    public class FakeItemService<TItem, TFaker> : IItemService<TItem>
        where TItem : Base
        where TFaker : Faker<TItem>
    {
        private ICollection<TItem> items;

        private readonly TFaker faker;

        public FakeItemService(TFaker faker)
        {
            this.faker = faker;

            items = faker.Generate(100);
        }

        public virtual void Add(TItem item) => items.Add(item);

        public virtual IEnumerable<TItem> Get() => items;

        public virtual TItem Get(int id) => items.SingleOrDefault(p => p.Id == id);

        public virtual void Remove(int id) => items.Remove(Get(id));
    }
}
