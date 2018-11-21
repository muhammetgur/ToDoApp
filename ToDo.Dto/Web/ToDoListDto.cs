using ToDo.Dto.Enums;

namespace ToDo.Dto.Web
{
    public class ToDoListDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Priority Priority { get; set; }

        public WorkFlow WorkFlow { get; set; }
    }
}
