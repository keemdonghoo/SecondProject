using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamProject.Models.Domain
{
    [Table("project_user")]

    public class User
    {
        
        public long UserId { get; set; }
        public string Name { get; set; }
        public string PassWord { get; set; }
        public string UserName { get; set; }
        public string PhoneNum { get; set; }
        public string NickName { get; set; }

        [DefaultValue(false)]
        public bool IsAdmin { get; set; }
      
        public string? Email { get; set; }

        public ICollection<Post> Posts { get; set; } = new HashSet<Post>();

        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();

        public ICollection<Favorite> Favorites { get; set; } = new HashSet<Favorite>();

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();    

        public ICollection<Like> Likes { get; set; } = new HashSet<Like>();



       


    }
}
