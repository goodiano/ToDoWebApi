namespace ToDo_Api.Dto_s
{
    public class ItemDto
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }
        public DateTime InsertTime { get; set; }
    }
}
