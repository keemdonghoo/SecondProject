namespace WebApplication1.Models.Domain
{
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
        public int ViewCnt { get; set; }
    }
}
