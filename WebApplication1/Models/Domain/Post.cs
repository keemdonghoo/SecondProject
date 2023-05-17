using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using TeamProject.Models.Domain;

namespace TeamProject.Models.Domain
{
    [Table("Post")]
    //게시글
    public class Post
    {
        public User User { get; set; }
        public long UserId { get; set; }
        public Board Board { get; set; }
        public long BoardId { get; set; }



        public int PostId  { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }



        [DefaultValue(0)]
        public int ViewCnt { get; set; }

        [DefaultValue(0)]
        public int LikeCnt { get; set; }



        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

        public ICollection<Attachment> Attachments { get; set; } = new HashSet<Attachment>();

        public ICollection<Like> Likes { get; set; } = new HashSet<Like>();



    }
}
