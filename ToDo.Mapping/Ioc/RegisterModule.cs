using System;
using System.Linq;
using AutoMapper;
using Unity.Extension;
using Unity.Lifetime;

namespace ToDo.Mapping.Ioc
{
    public class RegisterModule : UnityContainerExtension
    {
        protected override void Initialize()
        {
            var mappingProfiles = typeof(ToDoListMappingProfile).Assembly.GetTypes().Where(t => typeof(Profile).IsAssignableFrom(t)).Select(t => (Profile)Activator.CreateInstance(t));
            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in mappingProfiles)
                {
                    cfg.AddProfile(profile);
                }
            });

            var mapper = config.CreateMapper();
            Container.RegisterInstance(typeof(IMapper), null, mapper, new SingletonLifetimeManager());
        }
    }
}
