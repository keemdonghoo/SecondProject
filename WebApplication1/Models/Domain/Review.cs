using System.ComponentModel.DataAnnotations.Schema;

namespace TeamProject.Models.Domain
{
    [Table("Review")]
    public class Review
    {
        public long Id { get; set; } 

        public float Rate { get; set; }

        public string Content { get; set; } 

        public DateTime Date { get; set; }

        public User User { get; set; }
        public long UserId { get; set; }

        public Movie Movie { get; set; }    
        public long MovieId { get; set; }  


    }
}
