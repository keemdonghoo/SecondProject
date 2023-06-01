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

        //public string poster_path { get; set; }

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
