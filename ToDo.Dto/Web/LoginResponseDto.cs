namespace ToDo.Dto.Web
{
    public class LoginResponseDto :ResultDto
    {
        public string SessionToken { get; set; }

        public UserDto User { get; set; }
    }
}
