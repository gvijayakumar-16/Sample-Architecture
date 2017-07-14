using AutoMapper;
using Blogging.Core;
using Blogging.Web.Models.Blogs;
using System;

namespace Blogging.Web.Infrastructure.Mapper
{
    public class ModelMapperConfiguration : IMapperConfiguration
    {
        /// <summary>
        /// Get configuration
        /// </summary>
        /// <returns>Mapper configuration action</returns>
        public Action<IMapperConfigurationExpression> GetConfiguration()
        {
            Action<IMapperConfigurationExpression> action = cfg =>
            {
                cfg.CreateMap<BlogEditModel, Blog>()
                .ForMember(dest => dest.BlogId, m => m.MapFrom(x => x.Id));

                cfg.CreateMap<Blog, BlogEditModel>()
                .ForMember(dest => dest.Id, m => m.MapFrom(x => x.BlogId));
            };

            return action;
        }

        public int Order
        {
            get
            {
                return 0;
            }
        }
    }
}