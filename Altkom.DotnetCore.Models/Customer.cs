using System;

namespace Altkom.DotnetCore.Models
{
    public class Customer : Base
    {
        // snippet: prop
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string EMail { get; set; }
        public bool IsDeleted { get; set; }

        public bool IsSelected { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
