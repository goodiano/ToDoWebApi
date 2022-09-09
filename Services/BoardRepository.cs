using ToDo_Api.Models.Context;

namespace ToDo_Api.Services
{
    public class BoardRepository
    {
        private readonly DataBaseContext _context;
        public BoardRepository(DataBaseContext context)
        {
            _context = context;
        }


    }
}
