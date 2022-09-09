namespace ToDo_Api.Dto_s
{
    public class ToDoItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }
        public DateTime InsertTime { get; set; }
        public List<LinkDto> Links { get; set; }
    }
}
