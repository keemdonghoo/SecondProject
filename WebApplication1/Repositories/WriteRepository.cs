using WebApplication1.Data;
using WebApplication1.Models.Domain;

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

        public Task<Post> AddPostAsync(Post post)
        {
            throw new NotImplementedException();
        }

        public Task<Review> AddReviewAsync(Review review)
        {
            throw new NotImplementedException();
        }

        public Task<Post?> DeletePostAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Board?>> GetAllPostAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Review?> GetIdReviewAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> GetMovieDetailAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> GetNowMovieAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Post?> GetPostAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Like> IncLikeCntAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Post?> IncViewCntAsync()
        {
            //var existingWrite = await movieDbContext.
            //if (existingWrite == null) return null;

            //existingWrite.ViewCnt++;
            //await movieDbContext.SaveChangesAsync();
            //return existingWrite;
        }

        public Task<Post?> UpdatePostAsync(Post post)
        {
            throw new NotImplementedException();
        }
    }
}
