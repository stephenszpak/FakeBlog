using FakeBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBlog.DAL
{
    public interface IRepository
    {
        //create
        void AddAuthor(string name, ApplicationUser Owner);
        void AddWork(string name, Author owner);


        //read
        PublishedWork GetPublishedWork(int PublishedWorkId);
        List<PublishedWork> GetAuthorsPublishedWork(string AuthorId);

        //update
        bool IsADraft(int PublishedWorkId);
        bool IsEdited(int PublishedWorkId);

        //delete
        bool RemovePublishedWork(int PublishedWorkId);
        bool RemoveAuthor(int AuthorId);
    }
}
 