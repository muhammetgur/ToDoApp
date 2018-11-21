namespace ToDo.Dto.Web
{
    public class ResultDto<T>
    {
        public T Data { get; set; }

        public bool HasError { get; set; }

        public string Message { get; set; }

    }

    public class ResultDto
    {
        public bool HasError { get; set; }

        public string Message { get; set; }
    }
}
