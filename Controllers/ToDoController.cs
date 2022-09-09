using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo_Api.Dto_s;
using ToDo_Api.Models.Context;
using ToDo_Api.Models.Services;

namespace ToDo_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoRepository _todoRepository;
        private readonly DataBaseContext _context;
        public ToDoController(ToDoRepository toDo, DataBaseContext context)
        {
            _todoRepository = toDo;
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var todo = _todoRepository.GetAll().Select(p => new ToDoItemDto
            {
                Id = p.Id,
                Title = p.Title,
                Text = p.Text,
                IsDone = p.IsDone,
               InsertTime = p.InsertTime,
                Links = new List<LinkDto>()
                {
                     new LinkDto
                     {
                         Href =  Url.Action(nameof(Get),"ToDo",new {p.Id},Request.Scheme),
                         Rel = "Self",
                         Method = "GET",
                     },

                     new LinkDto
                     {
                         Href =  Url.Action(nameof(Delete),"ToDo",new {p.Id},Request.Scheme),
                         Rel = "Delete",
                         Method = "Delete",
                     },
                     new LinkDto
                     {
                         Href =  Url.Action(nameof(Put),"ToDo",new {p.Id},Request.Scheme),
                         Rel = "Edit",
                         Method = "Put",
                     },
                }
            }).ToList();
            return Ok(todo);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ItemDto itemDto)
        {   
            var todo = _todoRepository.Add(new AddToDoDto
            {
                ToDo = new ToDoDto
                {
                    Title = itemDto.Title,
                    Text = itemDto.Text,
                    IsDone = itemDto.IsDone,
                },

                links = new List<LinkDto>
                {
                    new LinkDto
                    {
                        Href =  Url.Action(nameof(Post),"ToDo",Request.Scheme),
                         Rel = "Self",
                         Method = "GET",
                    },
                    new LinkDto
                    {
                         Href =  Url.Action(nameof(Get),"ToDo",Request.Scheme),
                         Rel = "Get",
                         Method = "GET",
                    },
                    new LinkDto
                     {
                         Href =  Url.Action(nameof(Delete),"ToDo",Request.Scheme),
                         Rel = "Delete",
                         Method = "Delete",
                     },
                    new LinkDto
                     {
                         Href =  Url.Action(nameof(Put),"ToDo",Request.Scheme),
                         Rel = "Edit",
                         Method = "Put",
                     },
                }
            });

            string url = Url.Action(nameof(Post), "ToDo", todo.ToDo.Id, Request.Scheme);
            return Created(url, true);

        }

        [HttpPut]
        public IActionResult Put([FromBody] EditToDoDto editDto)
        {
            var result = _todoRepository.Edit(editDto);
            return StatusCode(200 , result);

        }

        [HttpGet("FailedToDo",Name = nameof(FaildToDo))]
        public IActionResult FaildToDo()
        {
            var result = _todoRepository.FailedTodo();
            return StatusCode(200, result);

        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var result = _context.ToDos.Find(Id);
            if(result == null)
            {
                return NotFound();
            }
             _todoRepository.Delete(Id);

            return Ok();

        }
    }
}
