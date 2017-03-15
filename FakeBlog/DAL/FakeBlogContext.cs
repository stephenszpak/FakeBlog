using FakeBlog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FakeBlog.DAL
{
    public class FakeBlogContext : ApplicationDbContext
    {
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<PublishedWork> PublishedWorks { get; set; }
    }
}