using System.ComponentModel.DataAnnotations.Schema;

namespace TeamProject.Models.Domain
{
    [Table("Board")]
    public class Board
    {
        //게시판 고유번호
        public long Id { get; set; } 
        //게시판 이름
        public string Name { get; set; }    
        //게시판은 여러개의 게시글이 생성될 수 있다
        public ICollection<Post> Posts { get; set; } = new HashSet<Post>(); 

    }
}
