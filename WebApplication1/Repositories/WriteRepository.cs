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

        public Task<Post> AddPost(Post post)
        {
            throw new NotImplementedException();
        }

        public Task<Review> AddReview(Review review)
        {
            throw new NotImplementedException();
        }

        public Task<Post?> DeletePost(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Board?>> GetAllPost()
        {
            throw new NotImplementedException();
        }

        public Task<Review?> GetIdReview(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> GetMovieDetail(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> GetNowMovie()
        {
            throw new NotImplementedException();
        }

        public Task<Post?> GetPost(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Like> IncLikeCnt()
        {
            throw new NotImplementedException();
        }

        public Task<Post?> IncViewCnt()
        {
            throw new NotImplementedException();
        }

        public Task<Post?> UpdatePost(Post post)
        {
            throw new NotImplementedException();
        }
    }
}
