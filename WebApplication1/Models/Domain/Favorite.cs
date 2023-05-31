using System.ComponentModel.DataAnnotations.Schema;

namespace TeamProject.Models.Domain
{
    [Table("Favorite")]
    public class Favorite
    {

        public long Id { get; set; }
        public string Name { get; set; }

     
        public long MovieId { get; set; }
        public Movie Movie { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }

    }
}
