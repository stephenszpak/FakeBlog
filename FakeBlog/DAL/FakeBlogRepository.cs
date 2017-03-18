using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FakeBlog.Models;

namespace FakeBlog.DAL
{
    public class FakeBlogRepository : IRepository
    {
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

        public PublishedWork GetPublishedWork(int PublishedWorkId)
        {
            PublishedWork found_work = Context.PublishedWorks.FirstOrDefault(b => b.PublishedWorkId == PublishedWorkId);
            return found_work;
        }

        public bool IsADraft(int PublishedWorkId)
        {
            throw new NotImplementedException();
        }

        public bool IsEdited(int PublishedWorkId)
        {
            throw new NotImplementedException();
        }

        public bool RemovePublishedWork(int PublishedWorkId)
        {
            PublishedWork found_work = GetPublishedWork(PublishedWorkId);

            if (found_work != null)
            {
                Context.PublishedWorks.Remove(found_work);
                Context.SaveChanges();
                return true;
            }
            return false;
        }

        public void AddWork(string name, Author owner)
        {
            PublishedWork newWork = new PublishedWork { Name = name, Author = owner };

            Context.PublishedWorks.Add(newWork);
            Context.SaveChanges();
        }

        public List<PublishedWork> GetAuthorsPublishedWork(string AuthorId)
        {
            return Context.PublishedWorks.Where(b => b.Owner.Id == AuthorId).ToList();
        }
    }
}