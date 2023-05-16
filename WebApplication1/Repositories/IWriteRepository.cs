using WebApplication1.Models.Domain;

namespace WebApplication1.Repositories
{
    public interface IWriteRepository
    {
        //현재 상영중인 영화 목록 불러오기
        Task<IEnumerable<Movie>> GetNowMovieAsync();
        //특정 영화의 상세정보 불러오기
        Task<Movie> GetMovieDetailAsync(long id);

        //특정 게시글의 댓글 작성하기
        Task<Review> AddReviewAsync(Review review);
        //특정 게시글의 댓글 불러오기 
        Task<Review?> GetIdReviewAsync(long id);

        //게시글 목록 불러오기
        Task<IEnumerable<Board?>> GetAllPostAsync(); 
        //새로운 게시글 생성하기
        Task<Post> AddPostAsync(Post post);

        //특정 ID의 게시글 읽어오기
        Task<Post?> GetPostAsync(long id);
        //특정 게시글 수정하기
        Task<Post?> UpdatePostAsync(Post post);
        //특정 게시글 삭제하기
        Task<Post?> DeletePostAsync(long id);
        //특정 게시글 조회수 +1
        Task<Post?> IncViewCntAsync();
        //특정 게시글 좋아요 +1
        Task<Like> IncLikeCntAsync();

        //관리자 
    }
}
