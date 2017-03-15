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


        [TestInitialize]
        public void Setup()
        {
            fake_work_table = new List<PublishedWork>();
            fake_context = new Mock<FakeBlogContext>();
            mock_work_set = new Mock<DbSet<PublishedWork>>();
            repo = new FakeBlogRepository(fake_context.Object);
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

            //Assert
            Assert.AreEqual(1, repo.Context.PublishedWorks.Count());

        }
    }
}
