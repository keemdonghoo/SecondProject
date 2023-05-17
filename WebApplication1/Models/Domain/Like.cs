using System.ComponentModel.DataAnnotations.Schema;

namespace TeamProject.Models.Domain
{
    [Table("Like")]
    public class Like
    {
        public User User { get; set; }
        public long UserId {get; set;}

        public Post Post { get; set; }
        public long PostId { get; set;}

         

       
    }
}
