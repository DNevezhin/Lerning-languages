using Microsoft.EntityFrameworkCore;
using LerningLanguages.Models;

namespace LerningLanguages.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
