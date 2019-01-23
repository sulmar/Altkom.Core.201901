using Altkom.DotnetCore.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.DotnetCore.WebApp.Hubs
{
    public class CustomersHub : Hub
    {
        public Task AddedCustomer(Customer customer)
        {
            return this.Clients.Others.SendAsync("Added", customer);
        }

        public override Task OnConnectedAsync()
        {
            this.Clients.Caller.SendAsync("Added", "Hello!");

            this.Groups.AddToGroupAsync(this.Context.ConnectionId, "Altkom");

            return base.OnConnectedAsync();


        }
    }
}
