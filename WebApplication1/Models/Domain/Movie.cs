using System.ComponentModel.DataAnnotations.Schema;

namespace TeamProject.Models.Domain
{
    [Table("Movie")]
    public class Movie
    {
        //영화 고유 Id
        public long Id { get; set; }  
        //영화제목
        public string Title { get; set; }   
        //개봉날짜
        public DateTime RegDate { get; set; }   
        //줄거리
        public string OverView { get; set; }
        //영화 장르 (1개 이상)
        public string Genre { get; set; } 
        //영화 포스터 url
        public string PostPath { get; set; }
        //영화 평균 평점
        public float RateAvg { get; set; }  


        //영화 좋아요와 1:N
        public ICollection<Favorite> Favorites { get; set; } = new HashSet<Favorite>();

    }
}
