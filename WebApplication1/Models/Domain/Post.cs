using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamProject.Models.Domain
{
    [Table("Post")]
    //게시글
    public class Post
    {
     
        //게시글 고유번호
        public long Id  { get; set; }
        //게시글 제목
        public string Title { get; set; }
        //게시글 내용
        public string Content { get; set; }
        //게시글 작성시간
        public DateTime Date { get; set; }

        //조회수 (기본0)
        [DefaultValue(0)]
        public int ViewCnt { get; set; }

        //좋아요 숫자(기본0)
        [DefaultValue(0)]
        public int LikeCnt { get; set; }

        //사용자는 여러개의 게시글을 작성할 수 있다
        public User User { get; set; }
        public long UserId { get; set; }
        //게시판은 여러개의 게시글이 있을 수 있다
        public Board Board { get; set; }
        public long BoardId { get; set; }



        //게시글은 여러개의 첨부파일을 가질 수 있다
        public ICollection<Attachment> Attachments { get; set; } = new HashSet<Attachment>();
        //게시글은 여러개의 좋아요를 가질 수 있다. (LikeCnt로 좋아요 받은 숫자 카운트)
        public ICollection<Like> Likes { get; set; } = new HashSet<Like>();
        //게시글은 여러개의 댓글이 달릴 수 있다.
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
