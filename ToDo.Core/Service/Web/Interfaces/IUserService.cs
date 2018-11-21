using ToDo.Dto.Web;

namespace ToDo.Core.Service.Web.Interfaces
{
    public interface IUserService
    {
        RegisterResponseDto Register(UserDto userDto);

        ResultDto<UserDto> Get(int id);

        LoginResponseDto Login(LoginDto loginDto);

        ResultDto<UserDto> GetBySessionToken(string sessionToken);
    }
}
