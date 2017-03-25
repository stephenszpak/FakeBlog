using FakeBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBlog.DAL
{
    public interface IPublishedWork
    {
        //create
        void AddWork(string name, string body, ApplicationUser owner);

        //read
        PublishedWork GetPublishedWork(int publishedWorkId);

        //update
        void EditBlog(int publishedWorkId, string editedBody);

        //delete
        bool RemovePublishedWork(int publishedWorkId);
    }
}
 