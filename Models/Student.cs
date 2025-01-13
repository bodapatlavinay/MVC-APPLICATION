using System;
using System.Collections.Generic;

namespace STUDB.Models
{
    public partial class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int? Age { get; set; }
        public string? Address { get; set; }
    }
}
