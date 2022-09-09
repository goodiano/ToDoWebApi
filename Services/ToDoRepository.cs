using ToDo_Api.Dto_s;
using ToDo_Api.Models.Context;
using ToDo_Api.Models.Entities;

namespace ToDo_Api.Models.Services
{
    public class ToDoRepository
    {
        private readonly DataBaseContext _context;
        public ToDoRepository(DataBaseContext context)
        {
            _context = context;
        }


        public List<ToDoDto> GetAll()
        {
            var result = _context.ToDos
                .Select(p => new ToDoDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Text = p.Text,
                    InsertTime = p.InsertTime,
                    IsDone = p.IsDone
                }).ToList();

            return result;
        }

        public ToDoDto Get(int Id)
        {
            var todo = _context.ToDos.SingleOrDefault(p => p.Id == Id);
            return new ToDoDto
            {
                Id = todo.Id,
                Title = todo.Title,
                Text = todo.Text,
                IsDone = todo.IsDone,
                InsertTime = todo.InsertTime,
                IsRemoved = todo.IsRemoved

            };
        }

        public AddToDoDto Add(AddToDoDto toDo)
        {
            ToDo newToDo = new ToDo
            {
                Title = toDo.ToDo.Title,
                Text = toDo.ToDo.Text,
                IsDone = toDo.ToDo.IsDone,
                InsertTime = DateTime.Now
            };

            foreach (var item in toDo.boards)
            {
                var board = _context.Boards.SingleOrDefault(p => p.Id == item);
                newToDo.Boards.Add(board);
            }
            _context.ToDos.Add(newToDo);
            _context.SaveChanges();

            return new AddToDoDto
            {
                ToDo = new ToDoDto()
                {
                    Title = newToDo.Title,
                    Text = newToDo.Text,
                    IsDone = newToDo.IsDone,
                    InsertTime = newToDo.InsertTime
                },
                boards = toDo.boards
            };
        }

        public void Delete(int Id)
        {
            var todo = _context.ToDos.Find(Id);
            if (todo != null)
            {
                todo.IsRemoved = true;
                _context.SaveChanges();
            }
        }

        public bool Edit(EditToDoDto editToDo)
        {
            var todo = _context.ToDos.SingleOrDefault(p => p.Id == editToDo.Id);
            todo.Title = editToDo.Title;
            todo.Text = editToDo.Text;
            todo.IsDone = editToDo.IsDone;
            todo.UpdateTime = DateTime.Now;
            _context.SaveChanges();
            return true;
        }

        public List<ToDoDto> FailedTodo()
        {
            var result = _context.ToDos
                .Where(p => p.IsDone == false)
                .Select(p => new ToDoDto
                {
                    Id=p.Id,
                    Title = p.Title,
                    Text = p.Text,
                    InsertTime = p.InsertTime
                }).ToList();
            return result;


        }

    }



    public class ToDoDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }
        public DateTime InsertTime { get; set; }
        public bool IsRemoved { get; set; }
    }

    public class AddToDoDto
    {
        public ToDoDto ToDo { get; set; }
        public List<int> boards { get; set; } = new List<int>();
        public List<LinkDto> links { get; set; }

    }

    public class EditToDoDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }
        public DateTime updateTime { get; set; }
        public List<int> boards { get; set; }
        public List<LinkDto> links { get; set; }

    }


}
