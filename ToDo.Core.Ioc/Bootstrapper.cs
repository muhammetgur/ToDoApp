using System.Collections.Generic;
using System.Linq;
using ToDo.Core.Data;
using ToDo.Mapping.Ioc;
using Unity;
using Unity.Interception.ContainerIntegration;
using Unity.Resolution;

namespace ToDo.Core.Ioc
{
    public class Bootstrapper
    {
        public static IUnityContainer Container { get; private set; }

        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            Container = container;

            return container;
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        public static T Resolve<T>(params KeyValuePair<string, object>[] parameters)
        {
            ParameterOverrides ss = new ParameterOverrides();
            parameters.ToList().ForEach(t => ss.Add(t.Key, t.Value));
            return Container.Resolve<T>(ss);
        }

        public static T Resolve<T>(string name, params KeyValuePair<string, object>[] parameters)
        {
            ParameterOverrides ss = new ParameterOverrides();
            parameters.ToList().ForEach(t => ss.Add(t.Key, t.Value));
            return Container.Resolve<T>(name, ss);
        }

        private static IUnityContainer BuildUnityContainer()
        {
            Container = new UnityContainer();
            RegisterTypes(Container);
            return Container;
        }

        public static void RegisterInstance<T>(T instence)
        {
            Container.RegisterInstance(instence);
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.AddNewExtension<Interception>();

            Container.RegisterType<ToDoListEntities>(new Unity.Lifetime.SingletonLifetimeManager());

            container.AddExtension(new RegisterModule());

            container.AddExtension(new RepositoryRegisterModule());

            container.AddExtension(new ServiceRegisterModule());

            container.AddNewExtension<Interception>();

        }
    }
}
