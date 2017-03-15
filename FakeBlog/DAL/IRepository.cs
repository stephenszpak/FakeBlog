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
        List<PublishedWork> GetAllPublishedWork(int PublishedWorkId);
        List<PublishedWork> GetAuthorsPublishedWork(string userId);

        //update
        bool IsADraft(int PublishedWorkId);
        bool IsEdited(int PublishedWorkId);

        //delete
        bool RemovePublishedWork(int PublishedWorkId);
        bool RemoveAuthor(int AuthorId);



    }
}
