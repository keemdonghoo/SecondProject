using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.Domain
{
    [Table("Movie")]
    public class Movie
    {
        //영화 고유 ID
        public long MoiveUID { get; set; }  
        //영화 제목
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
