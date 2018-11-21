using AutoMapper;
using ToDo.Core.Repository;

namespace ToDo.Core.Service.Web
{
    internal abstract class BaseService
    {
        internal readonly IMapper Mapper;
        internal readonly ToDoUnitOfWork ToDoUnitOfWork;
        protected BaseService(IMapper mapper, ToDoUnitOfWork toDoUnitOfWork)
        {
            Mapper = mapper;
            ToDoUnitOfWork = toDoUnitOfWork;
        }
    }
}
