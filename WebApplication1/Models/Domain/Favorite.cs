using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.Domain
{
    [Table("Favorite")]
    public class Favorite
    {
        public long FavoriteUID { get; set; }

        public User User { get; set; }
        public long UserUID { get; set; }

        public string Name { get; set; }   
        
        public ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();
    }
}
