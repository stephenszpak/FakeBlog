using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FakeBlog.Models
{
    public class PublishedWork
    {
        [Key]
        public int PublishedWorkId { get; set; }

        public int DraftId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Draft> Drafts { get; set; }

        public ICollection<DeletePublishedWork> DeleteWorks { get; set;}
    }
}