using Microsoft.EntityFrameworkCore;
using TeamProject.Models.Domain;

namespace TeamProject.Data
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext()
        {
        }

        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public DbSet<Attachment> Attachments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                  .HasOne(c => c.User)
                  .WithMany()
                  .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Like>().HasKey(l => new { l.UserId, l.PostId });

            modelBuilder.Entity<Like>()
                             .HasOne(c => c.User)
                             .WithMany()
                             .OnDelete(DeleteBehavior.NoAction);

           


            BuildSeed(modelBuilder);

        }

        private void BuildSeed(ModelBuilder modelBuilder)
        {
            User admin1 = new() { Id = 1, UserName = "Admin1", PassWord = "1234", Name = "주인장", PhoneNum = "01011111111", NickName = "주인장", IsAdmin = true, Email = "admin@ggg.aaa" };
            User user1 = new() { Id = 2, UserName = "User1", PassWord = "1234", Name = "일반회원", PhoneNum = "01022222222", NickName = "일회1", IsAdmin = false, Email = "ilhoho@ggg.aaa" };
            User user2 = new() { Id = 3, UserName = "User2", PassWord = "1234", Name = "일반회원1", PhoneNum = "01022222223", NickName = "일회2", IsAdmin = false, Email = "ilhoho@ggg.bbb" };

            Board board1 = new() { Id = 1, Name = "자유게시판" };

            var users = new List<User> { admin1, user1, user2 };
            var boards = new List<Board> { board1 };

            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Board>().HasData(boards);
        }



    }
}
