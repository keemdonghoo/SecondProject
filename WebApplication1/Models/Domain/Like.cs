using System.ComponentModel.DataAnnotations.Schema;

namespace TeamProject.Models.Domain
{
    [Table("Like")]
    public class Like
    {
        public long UserId {get; set;}
        public long PostId { get; set;}

        public User User{ get; set; }   

        public Post Post { get; set; }   
    }
}
