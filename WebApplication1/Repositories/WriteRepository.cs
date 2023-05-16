using Microsoft.EntityFrameworkCore;
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

        public async Task<Post> AddPostAsync(Post post)
        {
            throw new NotImplementedException();
        }

        public async Task<Review> AddReviewAsync(Review review)
        {
            throw new NotImplementedException();
        }

        public async Task<Post?> DeletePostAsync(long uid)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Board?>> GetAllPostAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Review?> GetIdReviewAsync(long uid)
        {
            throw new NotImplementedException();
        }

        public async Task<Movie> GetMovieDetailAsync(long uid)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Movie>> GetNowMovieAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Post?> GetPostAsync(long uid)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ToggleLikeAsync(long userUid, long postUid)
        {
            // UserUID와 PostUID로 기존의 Like를 찾는다.
            var existingLike = await movieDbContext.Likes
                .FirstOrDefaultAsync(l => l.UserUID == userUid && l.PostUID == postUid);

            // 만약 Like가 이미 있다면, 삭제한다.
            if (existingLike != null)
            {
                movieDbContext.Likes.Remove(existingLike);
            }
            else // 그렇지 않으면 새로운 Like를 추가한다.
            {
                var newLike = new Like { UserUID = userUid, PostUID = postUid };
                await movieDbContext.Likes.AddAsync(newLike);
            }

            // 변경 사항을 저장한다.
            await movieDbContext.SaveChangesAsync();

            // 변경 후의 상태를 반환한다.
            // Like가 삭제되었다면 false를, 추가되었다면 true를 반환한다.
            return existingLike == null;
        }


        public async Task<Post?> IncViewCntAsync(long uid)
        {
            var existingWrite = await movieDbContext.Posts.FindAsync(uid);
            if (existingWrite == null) return null;

            existingWrite.ViewCnt++;
            await movieDbContext.SaveChangesAsync();
            return existingWrite;
        }

        public async Task<Post?> UpdatePostAsync(Post post)
        {
            throw new NotImplementedException();
        }
    }
}
