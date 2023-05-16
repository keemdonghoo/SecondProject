using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
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

        //특정 MovieUID의 영화 상세정보 가져오기
        public async Task<Movie> GetMovieDetailAsync(long uid)
        {
            throw new NotImplementedException();
        }

        //현재 상영중인 영화 목록 가져오기
        public async Task<IEnumerable<Movie>> GetNowMovieAsync()
        {
            throw new NotImplementedException();
        }

        //특정 UserUID의 게시글 가져오기
        public async Task<Post?> GetPostAsync(long uid)
        {
            return await movieDbContext.Posts.FirstOrDefaultAsync(x => x.UserId == uid);
        }

        //좋아요 토글
        public async Task<bool> ToggleLikeAsync(long userUid, long postUid)
        {
            // UserUID와 PostUID로 기존의 Like를 찾는다.
            var existingLike = await movieDbContext.Likes
                .FirstOrDefaultAsync(l => l.UserId == userUid && l.PostId == postUid);

            // 만약 Like가 이미 있다면, 삭제한다.
            if (existingLike != null)
            {
                movieDbContext.Likes.Remove(existingLike);
            }
            else // 그렇지 않으면 새로운 Like를 추가한다.
            {
                var newLike = new Like { UserId = userUid, PostId = postUid };
                await movieDbContext.Likes.AddAsync(newLike);
            }

            // 변경 사항을 저장한다.
            await movieDbContext.SaveChangesAsync();

            // 변경 후의 상태를 반환한다.
            // Like가 삭제되었다면 false를, 추가되었다면 true를 반환한다.
            return existingLike == null;
        }

        //조회수 증가
        public async Task<Post?> IncViewCntAsync(long uid)
        {
            var existingWrite = await movieDbContext.Posts.FindAsync(uid);
            if (existingWrite == null) return null;

            existingWrite.ViewCnt++;
            await movieDbContext.SaveChangesAsync();
            return existingWrite;
        }

        //특정 PostUID의 게시글 수정하기
        public async Task<Post?> UpdatePostAsync(Post post)
        {
            var existingWrite = await movieDbContext.Posts.FindAsync(post.PostId);
            if (existingWrite == null) return null;

            existingWrite.Title = post.Title;
            existingWrite.Content = post.Content;

            await movieDbContext.SaveChangesAsync();  // UPDATE
            return existingWrite;
        }
    }
}
