using System;
using System.Linq;
using AutoMapper;
using ToDo.Core.Data;
using ToDo.Core.Repository;
using ToDo.Core.Service.Web.Interfaces;
using ToDo.Dto.Enums;
using ToDo.Dto.Web;

namespace ToDo.Core.Service.Web
{
    internal class UserService : BaseService, IUserService
    {
        public UserService(IMapper mapper, ToDoUnitOfWork toDoUnitOfWork) : base(mapper, toDoUnitOfWork)
        {
        }

        public RegisterResponseDto Register(UserDto userDto)
        {
            if (ToDoUnitOfWork.UserRepository.Where(x => x.Email == userDto.Email && x.Status == (int)Status.Active).ToList().Any())
                return new RegisterResponseDto
                {
                    HasError = true,
                    Message = "Mail adresi ile kayıtlı kullanıcı bulunmaktadır!"
                };

            var sessionToken = Guid.NewGuid().ToString();
            var user = Mapper.Map<User>(userDto);
            user.Status = (int)Status.Active;

            ToDoUnitOfWork.UserRepository.Insert(user);
            ToDoUnitOfWork.Save();

            ToDoUnitOfWork.UserTokenRepository.Insert(new UserToken { Token = sessionToken, UserId = user.Id });
            ToDoUnitOfWork.Save();

            var result = new RegisterResponseDto
            {
                User = Mapper.Map<UserDto>(user),
                SessionToken = sessionToken,
                HasError = false
            };
            return result;
        }

        public ResultDto<UserDto> Get(int id)
        {
            var user = ToDoUnitOfWork.UserRepository.Where(x => x.Id == id && x.Status == (int)Status.Active)
                .SingleOrDefault();

            return new ResultDto<UserDto>
            {
                Data = Mapper.Map<UserDto>(user),
                HasError = user == null,
                Message = user == null ? "Kullanıcı bulunamadı!" : null,
            };
        }

        public LoginResponseDto Login(LoginDto loginDto)
        {
            var user = ToDoUnitOfWork.UserRepository.Where(x => x.Email == loginDto.Email && x.Password == loginDto.Password && x.Status == (int)Status.Active)
                .SingleOrDefault();
            if (user == null)
            {
                return new LoginResponseDto
                {
                    HasError = true,
                    Message = "Hatalı kullanıcı adı yada şifre!"
                };
            }

            return CreateUserSession(user.Id);
        }

        public ResultDto<UserDto> GetBySessionToken(string sessionToken)
        {
            throw new NotImplementedException();
        }

        private LoginResponseDto CreateUserSession(int userId)
        {
            var userSession = new UserToken
            {
                UserId = userId,
                Token = Guid.NewGuid().ToString(),
                ExpireDate = DateTime.Now.AddDays(2)
            };

            ToDoUnitOfWork.UserTokenRepository.Insert(userSession);
            ToDoUnitOfWork.Save();

            var response = new LoginResponseDto
            {
                User = Mapper.Map<UserDto>(userSession.User),
                SessionToken = userSession.Token,
                HasError = false
            };
            return response;
        }
    }
}
