using Blogging.Core;
using Blogging.Data.Repository;
using System;
using System.Collections.Generic;

namespace Blogging.Services.Services.Blogs
{
    public class BlogService : IBlogService
    {
        #region PrivateFields

        private IRepository<Blog> _blogRepository;

        #endregion

        #region constructor

        public BlogService(IRepository<Blog> blogRepository)
        {
            _blogRepository = blogRepository;
        }

        #endregion

        #region methods

        /// <summary>
        /// Get the blog based on the id
        /// </summary>
        /// <param name="blogID">Blog ID</param>
        /// <returns></returns>
        public Blog GetBlogByID(int blogID)
        {
            if (blogID <= 0) throw new ArgumentException();
            return _blogRepository.GetById(blogID);
        }

        /// <summary>
        /// Save/Update the blog to DB
        /// </summary>
        /// <param name="model">The blog details</param>
        public void SaveBlog(Blog model)
        {
            if (model == null)
                throw new ArgumentNullException();
            if (model.BlogId > 0)
                _blogRepository.Update(model);
            else
                _blogRepository.Insert(model);
        }

        /// <summary>
        /// Get all the blogs
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Blog> GetAllBlogs()
        {
            return _blogRepository.GetAllNoTracking();
        }

        #endregion
    }
}