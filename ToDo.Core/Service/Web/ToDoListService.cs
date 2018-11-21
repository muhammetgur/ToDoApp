using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ToDo.Core.Data;
using ToDo.Core.Repository;
using ToDo.Core.Service.Web.Interfaces;
using ToDo.Dto.Enums;
using ToDo.Dto.Web;

namespace ToDo.Core.Service.Web
{
    internal class ToDoListService : BaseService, IToDoListService
    {
        public ToDoListService(IMapper mapper, ToDoUnitOfWork toDoUnitOfWork) : base(mapper, toDoUnitOfWork)
        {
        }

        public ResultDto<ToDoListDto> Create(ToDoListDto toDolistDto)
        {
            var toDoList = Mapper.Map<ToDo_List>(toDolistDto);
            toDoList.Status = (int) Status.Active;
            ToDoUnitOfWork.ToDoListRepository.Insert(toDoList);
            ToDoUnitOfWork.Save();

            var result = new ResultDto<ToDoListDto>
            {
                Data = Mapper.Map<ToDoListDto>(toDoList),
                HasError = false
            };
            return result;
        }

        public ResultDto<List<ToDoListDto>> List(string searchText = null)
        {
            var list = ToDoUnitOfWork.ToDoListRepository.Where(x => x.Status == (int)Status.Active &&
                         (searchText == null || x.Title.Contains(searchText) || x.Description.Contains(searchText))).OrderBy(x=>x.Id).ToList();

            var result = new ResultDto<List<ToDoListDto>>
            {
                Data = Mapper.Map<List<ToDoListDto>>(list),
                HasError = false
            };

            return result;
        }

        public ResultDto Update(ToDoListDto toDoListDto)
        {
            var toDoList = ToDoUnitOfWork.ToDoListRepository
                .Where(x => x.Id == toDoListDto.Id && x.Status == (int)Status.Active).SingleOrDefault();

            if (toDoList == null)
                return new ResultDto { HasError = true, Message = "Böyle bir kayıt bulunamadı." };

            toDoList.Title = toDoListDto.Title;
            toDoList.Description = toDoListDto.Description;
            toDoList.Priority = (int) toDoListDto.Priority;
            toDoList.WorkFlow = (int) toDoListDto.WorkFlow;
            toDoList.UpdateDate = DateTime.Now;

            ToDoUnitOfWork.ToDoListRepository.Update(toDoList);
            ToDoUnitOfWork.Save();
            return new ResultDto { HasError = false };
        }

        public ResultDto<ToDoListDto> Get(int id)
        {
            var toDoList = ToDoUnitOfWork.ToDoListRepository.Where(x => x.Id == id && x.Status == (int)Status.Active)
                .SingleOrDefault();
            if (toDoList == null)
                return new ResultDto<ToDoListDto>
                {
                    Data = null,
                    HasError = true,
                    Message = "Böyle bir kayıt bulunamadı."
                };

            return new ResultDto<ToDoListDto>
            {
                Data = Mapper.Map<ToDoListDto>(toDoList),
                HasError = false
            };
        }

        public ResultDto Delete(int id)
        {
            var toDoList = ToDoUnitOfWork.ToDoListRepository.Where(x => x.Id == id && x.Status == (int)Status.Active)
                .SingleOrDefault();

            if (toDoList == null)
                return new ResultDto { HasError = true, Message = "Böyle bir kayıt bulunamadı." };

            toDoList.Status = (int)Status.Deleted;
            ToDoUnitOfWork.ToDoListRepository.Update(toDoList);
            ToDoUnitOfWork.Save();

            return new ResultDto { HasError = false };
        }
    }
}
