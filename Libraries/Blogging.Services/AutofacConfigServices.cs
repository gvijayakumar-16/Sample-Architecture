using Autofac;
using Blogging.Core;
using Blogging.Services.Services.Blogs;

namespace Blogging.Services
{
    public static class AutofacConfigServices
    {
        /// <summary>
        /// Register all services
        /// </summary>
        /// <param name="builder"></param>
        public static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<Blog>().AsSelf();
            builder.RegisterType<BlogService>().As<IBlogService>().InstancePerRequest();
        }

        /// <summary>
        /// Register the repositories in Data layer
        /// </summary>
        /// <param name="builder"></param>
        public static void RegisterData(ContainerBuilder builder)
        {
            Data.AutofacConfigData.RegisterRepository(builder);
        }
    }
}
