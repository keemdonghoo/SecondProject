using System.ComponentModel.DataAnnotations.Schema;

namespace TeamProject.Models.Domain
{
    [Table("Comment")]
    public class Comment
    {
        public long Id { get; set; }

        public string Content { get; set; } 

        public DateTime RegDate { get; set; }


        public User User { get; set; }
        public long UserId { get; set; }
             
        public Post Post { get; set; }
        public long PostId { get; set; }


    }
}
