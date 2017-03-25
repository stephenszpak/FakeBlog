using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FakeBlog.Models
{
    public class PublishedWork
    {
        [Key]
        public int PublishedWorkId { get; set; }

        [MinLength(3)]
        public string Name { get; set; }

        public string Body { get; set; }
        public DateTime DateCreated { get; set; } // Required by default
        public bool IsDraft { get; set; } 
        public ApplicationUser Owner { get; set; }
    }
}