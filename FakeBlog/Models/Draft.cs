using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FakeBlog.Models
{
    public class Draft
    {
        [Key]
        public int DraftId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<PublishedWork> PublishDraft { get; set; }

        public ICollection<EditDraft> Edit { get; set; }

        public ICollection<Author> Authors { get; set; }
    }
}