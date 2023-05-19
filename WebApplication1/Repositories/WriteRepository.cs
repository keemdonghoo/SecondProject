using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using TeamProject.Data;
using TeamProject.Models.Domain;

namespace TeamProject.Repositories
{
    public class WriteRepository : IWriteRepository
    {
        private readonly MovieDbContext movieDbContext;
        public WriteRepository(MovieDbContext movieDbContext)
        {
            Console.WriteLine("WriteRepoitory() 생성"); 
            this.movieDbContext = movieDbContext;
        }

        //새로운 게시글 생성하기
        public async Task<Post> AddPostAsync(Post post)
        {
            await movieDbContext.Posts.AddAsync(post);
            var changes = await movieDbContext.SaveChangesAsync();

            if (changes == 0)
            {
                throw new Exception("데이터베이스에 게시글을 추가하지 못했습니다 :(");
            }

            return post;
        }


        //특정 게시글의 댓글 작성하기
        public async Task<Comment> AddReviewAsync(Comment comment)
        {
            await movieDbContext.Comments.AddAsync(comment);
            var changes = await movieDbContext.SaveChangesAsync();

            if (changes == 0)
            {
                throw new Exception("데이터베이스에 댓글을 추가하지 못했습니다.");
            }

            return comment;
        }

        //특정 게시글 삭제하기
        public async Task<Post?> DeletePostAsync(long postId)
        {
            var existingPost = await movieDbContext.Posts.FindAsync(postId);
            if (existingPost != null)
            {
                movieDbContext.Posts.Remove(existingPost);
                await movieDbContext.SaveChangesAsync();  // DELETE
                return existingPost;
            }
            return null;
        }

        //Board게시판의 게시글 목록 불러오기
        public async Task<List<Post>?> GetAllPostAsync(long boardId, int pageNum = 1, int pageSize = 10)
        {
            var posts = movieDbContext.Posts.Include(p => p.User).ToList();

            var board = await movieDbContext.Boards.Include(b => b.Posts).FirstOrDefaultAsync(b => b.Id == boardId);

            return board?.Posts
                .OrderByDescending(p => p.ViewCnt)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }


        //특정 게시글의 댓글 불러오기
        public async Task<List<Comment>?> GetIdReviewAsync(long postId)
        {
            var post = await movieDbContext.Posts.Include(p => p.Comments).FirstOrDefaultAsync(p => p.Id == postId);
            return post?.Comments.OrderByDescending(c => c.RegDate).ToList();
        }


        //특정 MovieUID의 영화 상세정보 가져오기
        public async Task<Movie> GetMovieDetailAsync(long uid)
        {
            throw new NotImplementedException();
        }


        //특정 UserId의 모든 게시글 가져오기
        public async Task<List<Post>> GetPostAsync(long userId)
        {
            return await movieDbContext.Posts.Where(x => x.UserId == userId).ToListAsync();
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

        //특정 PostId의 게시글 수정하기
        public async Task<Post?> UpdatePostAsync(Post post)
        {
            var existingWrite = await movieDbContext.Posts.FindAsync(post.Id);
            if (existingWrite == null) return null;

            movieDbContext.Entry(existingWrite).CurrentValues.SetValues(post);

            await movieDbContext.SaveChangesAsync();  // UPDATE
            return existingWrite;
        }

        //특정 Post Id의 게시글 하나 상세보기
        public async Task<Post?> GetPostDetailAsync(long id)
        {
            return await movieDbContext.Posts
                .Include(p => p.User)
                .Include(p => p.Board)
                .Include(p => p.Attachments)
                .Include(p => p.Likes)
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.Id == id);
        }


    }
}
