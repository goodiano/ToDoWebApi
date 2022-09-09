using Microsoft.EntityFrameworkCore;
using ToDo_Api.Models.Entities;

namespace ToDo_Api.Models.Context
{
    public class DataBaseContext:DbContext
    {
        public DataBaseContext(DbContextOptions options): base(options)
        {

        }
        public DbSet<Board> Boards { get; set; }
        public DbSet<ToDo> ToDos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDo>().HasQueryFilter(p=> !p.IsRemoved);
        }
    }

   
}
