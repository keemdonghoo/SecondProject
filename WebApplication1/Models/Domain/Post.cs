namespace WebApplication1.Models.Domain
{
    [Table("Post")]
    //게시글
    public class Post
    {
        public User User { get; set; }
        public long UserUID { get; set; }
        public Board Board { get; set; }
        public long BoardUID { get; set; }



        public int PostUID  { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        [DefaultValue(0)]
        public int ViewCnt { get; set; }

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

        public ICollection<Attachment> Attachments { get; set; } = new HashSet<Attachment>();

        public ICollection<Like> Likes { get; set; } = new HashSet<Like>();



        [DefaultValue(0)]
        public int LikeCnt { get; set; }
    }
}
