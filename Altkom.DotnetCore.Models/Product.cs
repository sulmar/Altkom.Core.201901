using System;
using System.Collections.Generic;
using System.Text;

namespace Altkom.DotnetCore.Models
{
    public class Product : Base
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
    }
}
