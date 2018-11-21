namespace ToDo.Dto.Web
{
    public class RegisterResponseDto :ResultDto
    {
        public UserDto User { get; set; }

        public string SessionToken { get; set; }
    }
}
