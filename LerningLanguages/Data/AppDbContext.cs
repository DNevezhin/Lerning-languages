using Microsoft.EntityFrameworkCore;
using LerningLanguages.Models;

namespace LerningLanguages.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LerningLanguages.Models.Task> Tasks { get; set; }

        public DbSet<UserProgress> UsersProgress { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
