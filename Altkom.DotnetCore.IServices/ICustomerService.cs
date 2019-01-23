using Altkom.DotnetCore.Models;
using System;
using System.Collections;

namespace Altkom.DotnetCore.IServices
{

    public interface ICustomerService
        : IItemService<Customer>
    {
        
    }

    public interface ICustomerServiceAsync
        : IItemServiceAsync<Customer>
    {
    }



}
