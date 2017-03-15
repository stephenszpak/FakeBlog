using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeBlog.DAL;
using Moq;
using System.Data.Entity;
using FakeBlog.Models;
using System.Linq;
using System.Collections.Generic;

namespace FakeBlog.Tests.FakeRepoTests
{
    [TestClass]
    public class FakeBlogRepoTests
    {
        public Mock<FakeBlogContext> fake_context { get; set; }
        public FakeBlogRepository repo { get; set; }
        public Mock<DbSet<PublishedWork>> mock_work_set { get; set; }
        public IQueryable<PublishedWork> query_work { get; set; }
        public List<PublishedWork> fake_work_table { get; set; }
        public ApplicationUser ted { get; set; }
        public ApplicationUser natalie { get; set; }


        [TestInitialize]
        public void Setup()
        {
            fake_work_table = new List<PublishedWork>();
            fake_context = new Mock<FakeBlogContext>();
            mock_work_set = new Mock<DbSet<PublishedWork>>();
            repo = new FakeBlogRepository(fake_context.Object);

            ted = new ApplicationUser { Id = "bear" };
            natalie = new ApplicationUser { Id = "naty" };
        }

        public void CreateFakeDatabase()
        {
            query_work = fake_work_table.AsQueryable();

            mock_work_set.As<IQueryable<PublishedWork>>().Setup(b => b.Provider).Returns(query_work.Provider);
            mock_work_set.As<IQueryable<PublishedWork>>().Setup(b => b.Expression).Returns(query_work.Expression);
            mock_work_set.As<IQueryable<PublishedWork>>().Setup(b => b.ElementType).Returns(query_work.ElementType);
            mock_work_set.As<IQueryable<PublishedWork>>().Setup(b => b.GetEnumerator()).Returns(() => query_work.GetEnumerator());

            mock_work_set.Setup(b => b.Add(It.IsAny<PublishedWork>())).Callback((PublishedWork PublishedWork) => fake_work_table.Add(PublishedWork));
            mock_work_set.Setup(b => b.Remove(It.IsAny<PublishedWork>())).Callback((PublishedWork PublishedWork) => fake_work_table.Remove(PublishedWork));
            fake_context.Setup(c => c.PublishedWorks).Returns(mock_work_set.Object);
        }



        [TestMethod]
        public void EnsureICanCreateInstance()
        {
            FakeBlogRepository repo = new FakeBlogRepository();

            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void EnsureContextIsNotNull()
        {
            FakeBlogRepository repo = new FakeBlogRepository();

            Assert.IsNotNull(repo.Context);
        }

        [TestMethod]
        public void EnsureICanInjectContextOfAnInstance()
        {
            FakeBlogContext context = new FakeBlogContext();
            FakeBlogRepository repo = new FakeBlogRepository(context);

            Assert.IsNotNull(repo.Context);
        }

        [TestMethod]
        public void EnsureICanPublishWork()
        {
            //Arrange
            CreateFakeDatabase();

            Author author_one = new Author
            {
                AuthorId = 2,
                FullName = "billy dily",
                Username = "Diibby"
            };

            //Act
            repo.AddWork("A Duck", author_one);
            repo.AddWork("A Duck", author_one);

            //Assert
            Assert.AreEqual(2, repo.Context.PublishedWorks.Count());

        }

        [TestMethod]
        public void EnsureICanReturnPublishedWork()
        {
            //Arrange
            fake_work_table.Add(new PublishedWork { AuthorId = 2, Name = "DisBook", PublishedWorkId = 1});
            CreateFakeDatabase();

            //Act
            int expected_board_count = 1;
            int actual_board_count = repo.Context.PublishedWorks.Count();

            //Assert
            Assert.AreEqual(expected_board_count, actual_board_count);

        }

        [TestMethod]
        public void EnsureICanGetPublishedWork()
        {
            //Arrange
            fake_work_table.Add(new PublishedWork { Name = "DisBook", PublishedWorkId = 1 });
            CreateFakeDatabase();

            //Act
            string expected_work_name = "DisBook";
            PublishedWork added_work = repo.GetPublishedWork(1);
            string actual_work_name = "DisBook";

            //Assert
            Assert.AreEqual(expected_work_name, actual_work_name);
        }

        [TestMethod]
        public void EnsureICanGetAuthorsPublishedWork()
        {
            //Arrange
            fake_work_table.Add(new PublishedWork { Name = "DisBook", PublishedWorkId = 1, Owner = ted });
            fake_work_table.Add(new PublishedWork { Name = "Disbook 4", PublishedWorkId = 2, Owner = natalie});
            fake_work_table.Add(new PublishedWork { Name = "DisBook 2", PublishedWorkId = 3, Owner = natalie });
            CreateFakeDatabase();

            //Act
            int expected_work_count = 2;
            int actual_work_count = repo.GetAuthorsPublishedWork(natalie.Id).Count;

            //Assert
            Assert.AreEqual(expected_work_count, actual_work_count);
        }

        [TestMethod]
        public void EnsureICanDeletePublishedWork()
        {
            //Arrange
            fake_work_table.Add(new PublishedWork { Name = "DisBook", PublishedWorkId = 1, Owner = ted });
            fake_work_table.Add(new PublishedWork { Name = "Disbook 4", PublishedWorkId = 2, Owner = natalie });
            fake_work_table.Add(new PublishedWork { Name = "DisBook 2", PublishedWorkId = 3, Owner = natalie });
            CreateFakeDatabase();

            //Act
            int expected_work_count = 2;
            repo.RemovePublishedWork(1);
            int actual_work_count = repo.Context.PublishedWorks.Count();

            //Assert
            Assert.AreEqual(expected_work_count, actual_work_count);
        }
    }
}
