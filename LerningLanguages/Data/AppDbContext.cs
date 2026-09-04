using Microsoft.EntityFrameworkCore;
using LerningLanguages.Models;

namespace LerningLanguages.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=app.db");
        }
    }
}
