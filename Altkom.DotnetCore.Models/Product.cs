using System;
using System.Collections.Generic;
using System.Text;

namespace Altkom.DotnetCore.Models
{
    public class Product : Base
    {
        public string Name { get; set; }
    }


    public class Article : Product
    {
        public string Color { get; set; }
        public float Weight { get; set; }
    }

    public class Service : Product
    {
        public TimeSpan Duration { get; set; }
    }
}
