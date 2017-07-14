using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blogging.Core;
using Blogging.Data.Repository;
using Blogging.Services.Services.Blogs;
using Rhino.Mocks;

namespace Blogging.Tests.Services
{
    [TestClass]
    public class BlogTests
    {
        private IRepository<Blog> _blogRepository;
        private IBlogService _blogService;

        [TestInitialize]
        public void SetUp()
        {
            _blogRepository = MockRepository.GenerateMock<IRepository<Blog>>();
            _blogService = new BlogService(_blogRepository);
        }

        [TestMethod]
        public void BlogGetByIDTest()
        {
            var blog = new Blog() { BlogId = 1, Url = "http://www.google.com" };
            _blogRepository.Stub(m => m.GetById(Arg<object>.Is.Anything)).Return(blog);
            var domainModel = _blogService.GetBlogByID(1);
            Assert.AreEqual(blog.Url, domainModel.Url);
            Assert.AreEqual(blog.BlogId, domainModel.BlogId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BlogGetByIDExceptionTest()
        {
            _blogService.GetBlogByID(-1);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BlogSaveNullTest()
        {
            _blogService.SaveBlog(null);
        }

        [TestMethod]
        public void BlogUpdateTest()
        {
            var blog = new Blog() { BlogId = 1, Url = "http://www.google.com" };
            _blogRepository.Expect(m => m.Update(Arg<Blog>.Is.NotNull, Arg<bool>.Is.Anything)).Repeat.Once();
            _blogService.SaveBlog(blog, true);
            _blogRepository.AssertWasCalled(m => m.Update(Arg<Blog>.Is.NotNull, Arg<bool>.Is.Anything), m => m.Repeat.Once());
        }

        [TestMethod]
        public void BlogInsertTest()
        {
            var blog = new Blog() { BlogId = 0, Url = "http://www.google.com" };
            _blogRepository.Expect(m => m.Insert(Arg<Blog>.Is.NotNull, Arg<bool>.Is.Anything)).Repeat.Once();
            _blogService.SaveBlog(blog, true);
            _blogRepository.AssertWasCalled(m => m.Insert(Arg<Blog>.Is.NotNull, Arg<bool>.Is.Anything), m => m.Repeat.Once());
        }
    }
}
