using Autofac;
using Blogging.Core;
using Blogging.Data.Repository;

namespace Blogging.Data
{
    public static class AutofacConfigData
    {
        public static void RegisterRepository(ContainerBuilder builder)
        {
            builder.RegisterType<BlogContext>().As<BloggingContext>().PropertiesAutowired().InstancePerRequest().OnRelease(i => i.Dispose());
            builder.RegisterGeneric(typeof(EFBaseRepository<>)).As(typeof(IRepository<>)).InstancePerRequest();
            builder.RegisterType<Blog>().AsSelf();
        }
    }
}