using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.Domain
{
    [Table("Comment")]
    public class Comment
    {
        public long CommentUID { get; set; }

        public string Content { get; set; } 

        public DateTime RegDate { get; set; }

        public User User { get; set; }
        public long UserUID { get; set; }

        public Post Post { get; set; }
        public long PostUID { get; set; }


    }
}
