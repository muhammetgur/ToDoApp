using AutoMapper;
using ToDo.Core.Data;
using ToDo.Dto.Web;

namespace ToDo.Mapping
{
    public class ToDoListMappingProfile : Profile
    {
        public ToDoListMappingProfile()
        {
            this.CreateMap<ToDoListDto, ToDo_List>();
            this.CreateMap<ToDo_List, ToDoListDto>();

            this.CreateMap<UserDto, User>();
            this.CreateMap<User, UserDto>();

        }
    }
}
