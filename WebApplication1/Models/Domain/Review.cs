using System.ComponentModel.DataAnnotations.Schema;

namespace TeamProject.Models.Domain
{
    [Table("Review")]
    public class Review
    {
        //리뷰 고유 아이디
        public long Id { get; set; } 
        //리뷰 평점
        public float Rate { get; set; }
        //리뷰 내용
        public string Content { get; set; } 
        //리뷰 작성날짜
        public DateTime Date { get; set; }
        
        public User User { get; set; }
        public long UserId { get; set; }

        public Movie Movie { get; set; }    
        public long MovieId { get; set; }  


    }
}
