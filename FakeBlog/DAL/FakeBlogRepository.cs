using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FakeBlog.Models;

namespace FakeBlog.DAL
{
    public class FakeBlogRepository : IRepository
    {
        private FakeBlogContext context;

        public FakeBlogContext Context { get; set; }
        public FakeBlogRepository()
        {
            Context = new FakeBlogContext();
        }

        public FakeBlogRepository(FakeBlogContext context)
        {
            Context = context;
        }

        public void AddAuthor(string name, ApplicationUser Owner)
        {
            throw new NotImplementedException();
        }

        

        public List<PublishedWork> GetAllPublishedWork(int PublishedWorkId)
        {
            throw new NotImplementedException();
        }

        public List<PublishedWork> GetAuthorsPublishedWork(string userId)
        {
            throw new NotImplementedException();
        }

        public bool IsADraft(int PublishedWorkId)
        {
            throw new NotImplementedException();
        }

        public bool IsEdited(int PublishedWorkId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveAuthor(int AuthorId)
        {
            throw new NotImplementedException();
        }

        public bool RemovePublishedWork(int PublishedWorkId)
        {
            throw new NotImplementedException();
        }

        public void AddWork(string name, Author owner)
        {
            PublishedWork newWork = new PublishedWork { Name = name, Author = owner };

            Context.PublishedWorks.Add(newWork);
            Context.SaveChanges();
        }
    }
}