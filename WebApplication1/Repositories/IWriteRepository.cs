using System.ComponentModel;
using TeamProject.Models.Domain;

namespace TeamProject.Repositories
{
    public interface IWriteRepository
    {
        //특정 영화의 상세정보 불러오기
        Task<Movie> GetMovieDetailAsync(long uid);

        //특정 게시글의 댓글 작성하기
        Task<Comment> AddCommentAsync(Comment comment);



		// 게시글에 첨부파일추가하기
		Task<Attachment> AddAttachmentAsync(Attachment attachment);

        // 게시글의 첨부파일 가져오기
        Task<List<Attachment>> GetAttachmentByPostIdAsync(long postId);

        Task<Attachment> GetAttachmentId(long attachmentId);

        //특정 댓글 삭제
        Task<Comment?> DeleteCommentAsync(long commentId);

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
        // 관리자 선택한 게시글들 삭제하기
        Task<Post> DeleteSelectedPosts(List<long> postIds);
        Task<Comment> DeleteSelectedComments(List<long> commentIds);
        //특정 게시글 조회수 +1
        Task<Post?> IncViewCntAsync(long uid);

        //특정 게시글 좋아요 토글 확인
        Task<bool> ToggleLikeAsync(long userUid, long postUid);

        //관리자용 모든 게시판 모든 게시글 목록
        Task<List<Post>?> AdminGetAllPostAsync();

        //관리자용 모든 댓글 목록
        Task<List<Comment>> AdminGetAllCommentAsync();


        Task<IEnumerable<Post>> GetFromRowAsync(long boardId, int fromRow, int pageRows);

        Task<long> CountAsync();

        //리뷰 작성
        //Task SaveReviewAsync(Review review, long movieId, string userId);
        Task SaveReviewAsync(Review review);
    }
}
