var connection = new signalR.HubConnectionBuilder().withUrl("/hubs/customers").build();

connection.on("Added", function (customer) {
    console.log(customer);
});

connection.start().catch(function (err) {
    console.log(err.toString());
});
