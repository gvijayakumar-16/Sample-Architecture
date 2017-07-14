using Blogging.Core;
using Blogging.Web.Infrastructure.Mapper;
using Blogging.Web.Models.Blogs;

namespace Blogging.Web.Extensions
{
    public static class MapperExtensions
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return AutoMapperConfiguration.Mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return AutoMapperConfiguration.Mapper.Map(source, destination);
        }

        #region Blogs

        public static BlogEditModel ToModel(this Blog entity)
        {
            return entity.MapTo<Blog, BlogEditModel>();
        }

        public static Blog ToEntity(this BlogEditModel model)
        {
            return model.MapTo<BlogEditModel, Blog>();
        }

        public static Blog ToEntity(this BlogEditModel model, Blog destination)
        {
            return model.MapTo(destination);
        }

        #endregion
    }
}