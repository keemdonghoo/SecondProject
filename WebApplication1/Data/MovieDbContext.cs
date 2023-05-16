using Microsoft.EntityFrameworkCore;
using TeamProject.Models.Domain;
using WebApplication1.Models.Domain;

namespace WebApplication1.Data
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Favorite> Favorites { get; set;}
        public DbSet<Like> Likes { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public DbSet<Attachment> Attachments { get; set; }
    }
}
