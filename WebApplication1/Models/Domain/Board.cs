using System.ComponentModel.DataAnnotations.Schema;

namespace TeamProject.Models.Domain
{
    [Table("Board")]
    public class Board
    {
        public long BoardId { get; set; } 

        public string Name { get; set; }    

        public ICollection<Post> Posts { get; set; } = new HashSet<Post>(); 

    }
}
