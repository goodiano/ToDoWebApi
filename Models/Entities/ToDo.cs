namespace ToDo_Api.Models.Entities
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }
        public DateTime InsertTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool IsRemoved { get; set; } = false;
        public ICollection<Board> Boards { get; set; }
    }
}
