using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamProject.Models.Domain
{
    [Table("User")]

    public class User
    {
        //사용자 고유번호
        public long Id { get; set; }
        //사용자 로그인 ID
        public string Name { get; set; }
        //사용자 로그인 비밀번호
        public string PassWord { get; set; }
        //사용자 실명(성명)
        public string UserName { get; set; }
        //사용자 전화번호
        public string PhoneNum { get; set; }
        //사용자 닉네임
        public string NickName { get; set; }

        //관리자 판단여부(디폴트:X)
        [DefaultValue(false)]
        public bool IsAdmin { get; set; }
        //사용자 이메일
        public string? Email { get; set; }

        //사용자는 여러개의 게시글을 작성할 수 있다
        public ICollection<Post> Posts { get; set; } = new HashSet<Post>();
        //사용자는 여러개의 리뷰를 남길 수 있다. (하나의 영화에는 하나의 리뷰. 여러 영화에 대하여 리뷰를 남길 수 있다)
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
        //사용자는 여러 영화에 대하여 각각 즐겨찾기 기능을 사용할 수 있다)
        public ICollection<Favorite> Favorites { get; set; } = new HashSet<Favorite>();
    }
}
