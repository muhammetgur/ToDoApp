using System.Runtime.CompilerServices;
using ToDo.Core.Data;
using Unity.Extension;
using Unity.Lifetime;

[assembly: InternalsVisibleTo("ToDo.Core.Ioc")]

namespace ToDo.Core.Ioc
{
    internal class RepositoryRegisterModule : UnityContainerExtension
    {
        protected override void Initialize()
        {
            RegisterClass<Repository.IGenericRepository<ToDo_List>, Repository.GenericRepository<ToDo_List>>();
            RegisterClass<Repository.IGenericRepository<User>, Repository.GenericRepository<User>>();
            RegisterClass<Repository.IGenericRepository<UserToken>, Repository.GenericRepository<UserToken>>();
        }

        private void RegisterClass<T, S>()
        {
            Container.RegisterType(typeof(T), typeof(S), null, new ContainerControlledLifetimeManager());
        }

    }
}
