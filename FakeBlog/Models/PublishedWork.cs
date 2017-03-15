using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FakeBlog.Models
{
    public class PublishedWork
    {
        [Key]
        public int PublishedWorkId { get; set; }

        [Required] //means data cannot be NULL
        [MinLength(3)]
        public string Name { get; set; }

        public string Description { get; set; }
        public DateTime DateCreated { get; set; } // Required by default
        public DateTime PublishedAt { get; set; }
        public bool IsDraft { get; set; }
        public bool IsEdited { get; set; }
        public ApplicationUser Owner { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }
    }
}