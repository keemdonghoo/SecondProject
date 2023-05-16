using WebApplication1.Data;

namespace WebApplication1.Repositories
{
    public class WriteRepository : IWriteRepository
    {
        private readonly MovieDbContext movieDbContext;
        public WriteRepository(MovieDbContext movieDbContext)
        {
            Console.WriteLine("WriteRepoitory() 생성");
            this.movieDbContext = movieDbContext;
        }
    }
}
