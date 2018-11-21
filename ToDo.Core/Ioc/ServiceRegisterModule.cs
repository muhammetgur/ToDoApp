using Unity.Extension;
using Unity.Lifetime;

namespace ToDo.Core.Ioc
{
    internal class ServiceRegisterModule : UnityContainerExtension
    {
        protected override void Initialize()
        {
            RegisterClass<Service.Web.Interfaces.IToDoListService, Service.Web.ToDoListService>();
            RegisterClass<Service.Web.Interfaces.IUserService, Service.Web.UserService>();
        }

        private void RegisterClass<T, S>()
        {
            Container.RegisterType(typeof(T), typeof(S), null, new ContainerControlledLifetimeManager());
        }

    }
}
