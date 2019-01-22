using Altkom.DotnetCore.IServices;
using Altkom.DotnetCore.Models;
using System;
using System.Linq;
using System.Text;

namespace Altkom.DotnetCore.FakeServices
{

    public class FakeProductService : FakeItemService<Product, ProductFaker>, IProductService
    {
        public FakeProductService(ProductFaker faker) : base(faker)
        {
        }
    }
}
