using System.ComponentModel.DataAnnotations.Schema;

namespace TeamProject.Models.Domain
{
    [Table("Favorite")]
    public class Favorite
    {
        public long Id { get; set; }

        public User User { get; set; }
        public long UserId { get; set; }

        public string Name { get; set; }   
        
        public ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();
    }
}
