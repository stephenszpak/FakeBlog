using System.ComponentModel.DataAnnotations;

namespace FakeBlog.Models
{
    public class DeletePublishedWork
    {
        public ApplicationUser AuthorName { get; set; }

        public int PublishedWorkId { get; set; }
    }
}