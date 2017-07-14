using Blogging.Core;
using System.Collections.Generic;

namespace Blogging.Services.Services.Blogs
{
    public interface IBlogService
    {
        /// <summary>
        /// Save/Update the blog to DB
        /// </summary>
        /// <param name="model">The blog details</param>
        /// <param name="saveChanges">(Optional) Persist changes to DB</param>
        void SaveBlog(Blog model, bool saveChanges = true);

        /// <summary>
        /// Get the blog based on the id
        /// </summary>
        /// <param name="blogID">Blog ID</param>
        /// <returns></returns>
        Blog GetBlogByID(int blogID);

        /// <summary>
        /// Get all the blogs
        /// </summary>
        /// <returns></returns>
        IEnumerable<Blog> GetAllBlogs();
    }
}