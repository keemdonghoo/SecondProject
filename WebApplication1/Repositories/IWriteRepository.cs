using TeamProject.Models.Domain;

namespace TeamProject.Repositories
{
    public interface IWriteRepository
    {
        //특정 영화의 상세정보 불러오기
        Task<Movie> GetMovieDetailAsync(long uid);

        //특정 게시글의 댓글 작성하기
        Task<Comment> AddCommentAsync(Comment comment);

        //특정 게시글의 댓글 불러오기
        Task<List<Comment>?> GetIdCommentAsync(long postId);

        //게시글 목록 불러오기
        Task<List<Post>?> GetAllPostAsync(long boardId);
        //새로운 게시글 생성하기
        Task<Post> AddPostAsync(Post post);

        //특정 ID의 게시글 읽어오기
        Task<Post> GetPostAsync(long postId);

        // 특정 유저Id의 게시글 목록
		Task<IEnumerable<Post>> GetUserPostAsync(long userId);

        // 특정 유저Id의 댓글 목록
        Task<IEnumerable<Comment>> GetUserCommentAsync(long userId);
        //특정 게시글 수정하기
        Task<Post?> UpdatePostAsync(Post post);
        //특정 게시글 삭제하기
        Task<Post?> DeletePostAsync(long postId);
        //특정 게시글 조회수 +1
        Task<Post?> IncViewCntAsync(long uid);

        //특정 게시글 좋아요 토글 확인
        Task<bool> ToggleLikeAsync(long userUid, long postUid);

        //관리자용 모든 게시판 모든 게시글 목록
        Task<List<Post>?> AdminGetAllPostAsync();

        //관리자용 모든 댓글 목록
        Task<List<Comment>> AdminGetAllCommentAsync();




        //관리자 
    }
}
