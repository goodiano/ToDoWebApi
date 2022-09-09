namespace ToDo_Api.Models.Entities
{
    public class Board
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime InsertTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool IsRemoved { get; set; } = false;
        public ICollection<ToDo> Boards { get; set; }
    }
}
