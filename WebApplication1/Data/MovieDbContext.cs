using Microsoft.EntityFrameworkCore;
using TeamProject.Models.Domain;

namespace TeamProject.Data
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
