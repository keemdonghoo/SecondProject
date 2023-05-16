namespace WebApplication1.Models.Domain
{
    public class Movie
    {
        public long MoiveUID { get; set; }  

        public string Title { get; set; }   

        public DateTime RegDate { get; set; }   

        public string OverView { get; set; }

        public string Genre { get; set; } 

        public string PostPath { get; set; }

        public float RateAvg { get; set; }  

    }
}
