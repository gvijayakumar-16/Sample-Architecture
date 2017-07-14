using AutoMapper;
using Blogging.Web.Infrastructure;
using Blogging.Web.Infrastructure.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blogging.Web.App_Start
{
    public static class AutomapperConfig
    {
        public static void Init()
        {
            var finder = new AppDomainTypeFinder();
            var mcTypes = finder.FindClassesOfType(typeof(IMapperConfiguration));
            var mcInstances = new List<IMapperConfiguration>();
            foreach (var mcType in mcTypes)
                mcInstances.Add((IMapperConfiguration)Activator.CreateInstance(mcType));

            //sort
            mcInstances = mcInstances.AsQueryable().OrderBy(t => t.Order).ToList();

            //get configurations
            var configurationActions = new List<Action<IMapperConfigurationExpression>>();
            foreach (var mc in mcInstances)
                configurationActions.Add(mc.GetConfiguration());

            //register
            AutoMapperConfiguration.Init(configurationActions);
        }
    }
}