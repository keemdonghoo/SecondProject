using System.ComponentModel.DataAnnotations.Schema;

namespace TeamProject.Models.Domain
{
    [Table("Movie")]
    public class Movie
    {
        public long Id { get; set; }  

        public string Title { get; set; }   
        //
        public DateTime RegDate { get; set; }   

        public string OverView { get; set; }

        public string Genre { get; set; } 

        public string PostPath { get; set; }

        public float RateAvg { get; set; }  

        public ICollection<Favorite> Favorites { get; set; } = new HashSet<Favorite>();

    }
}
