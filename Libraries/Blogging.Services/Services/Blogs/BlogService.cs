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
        /// <param name="saveChanges">(Optional) Persist changes to DB</param>
        public void SaveBlog(Blog model, bool saveChanges = true)
        {
            if (model == null)
                throw new ArgumentNullException();
            var domainModel = model;//AutoMapper.Mapper.Map<Blog>(model);
            if (domainModel.BlogId > 0)
                _blogRepository.Update(domainModel, saveChanges);
            else
                _blogRepository.Insert(domainModel, saveChanges);
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