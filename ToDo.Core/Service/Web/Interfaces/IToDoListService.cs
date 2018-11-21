using System.Collections.Generic;
using ToDo.Dto.Web;

namespace ToDo.Core.Service.Web.Interfaces
{
    public interface IToDoListService
    {
        ResultDto<ToDoListDto> Create(ToDoListDto toDolistDto);

        ResultDto<List<ToDoListDto>> List(string searchText = null);

        ResultDto Update(ToDoListDto toDoListDto);

        ResultDto<ToDoListDto> Get(int id);

        ResultDto Delete(int id);
    }
}
