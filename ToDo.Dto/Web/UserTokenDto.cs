using System;

namespace ToDo.Dto.Web
{
    public class UserTokenDto
    {
        public int Id { get; set; }

        public string Token { get; set; }

        public int UserId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ExpireDate { get; set; }
    }
}
