using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamProject.Models.Domain
{
    [Table("Movie")]
    public class Movie
    {
        //Movie Table Id
        public long Id { get; set; }
        //영화제목
        public string Title { get; set; }
        ////개봉날짜
        //public DateTime RegDate { get; set; }   
        ////줄거리
        //public string OverView { get; set; }
        ////영화 장르 (1개 이상)
        //public string Genre { get; set; } 
        ////영화 포스터 url
        //public string PostPath { get; set; }

        //영화 고유 ID
        public long MovieUid { get; set; }
        //영화 평균 평점
        [DefaultValue(0.0f)]
        public float RateAvg { get; set; }  


        //영화 좋아요와 1:N
        public ICollection<Favorite> Favorites { get; set; } = new HashSet<Favorite>();
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();

    }
}
