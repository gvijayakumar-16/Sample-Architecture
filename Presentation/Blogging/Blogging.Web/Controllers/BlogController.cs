using Blogging.Core;
using Blogging.Services.Services.Blogs;
using Blogging.Web.Models.Blogs;
using System.Web.Mvc;
using Blogging.Web.Extensions;

namespace Blogging.Web.Controllers
{
    public class BlogController : Controller
    {
        #region Private fields

        private readonly IBlogService _blogService;

        #endregion

        #region Constructor

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        #endregion

        #region Action Methods

        /// <summary>
        /// List all the blogs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult List()
        {
            var blogs = _blogService.GetAllBlogs();
            return View(blogs);
        }

        /// <summary>
        /// Get the blog for edit
        /// </summary>
        /// <param name="id">Blog ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var blog = _blogService.GetBlogByID(id);
            return View(blog.ToModel());
        }

        /// <summary>
        /// Save the blog
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BlogEditModel model)
        {
            var domainModel = _blogService.GetBlogByID(model.Id);
            _blogService.SaveBlog(model.ToEntity(domainModel));
            return RedirectToAction("List");
        }

        #endregion
    }
}