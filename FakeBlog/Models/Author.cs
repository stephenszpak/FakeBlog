using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FakeBlog.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        public string FullName { get; set; }

        public ApplicationUser AuthorName { get; set; }

        public ICollection<PublishedWork> Works { get; set; }


    }
}