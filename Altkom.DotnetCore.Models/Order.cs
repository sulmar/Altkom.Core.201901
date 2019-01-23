using System;
using System.Collections.Generic;
using System.Text;

namespace Altkom.DotnetCore.Models
{
    public class Order : Base
    {
        public string Number { get; set; }
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public IEnumerable<OrderDetail> Details { get; set; }

    }

    public class OrderDetail : Base
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
