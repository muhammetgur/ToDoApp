namespace ToDo.Web.Models
{
    public class ToDoListModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Priority { get; set; }

        public int WorkFlow { get; set; }
    }
}