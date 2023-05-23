using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using TeamProject.Models.Domain;

namespace TeamProject.Models.ViewModels
{
    public class CreatePostViewModel
    {
        //게시글 제목
        public string Title { get; set; }
        //게시글 내용
        public string Content { get; set; }
        //게시글 작성시간
        public DateTime Date { get; set; }

        //조회수 (기본0)
        [DefaultValue(0)]
        public int ViewCnt { get; set; }

        //좋아요 숫자(기본0)
        [DefaultValue(0)]
        public int LikeCnt { get; set; }

        //사용자는 여러개의 게시글을 작성할 수 있다
        public User User { get; set; }
        public long UserId { get; set; }
        //게시판은 여러개의 게시글이 있을 수 있다
        public Board Board { get; set; }
        public long BoardId { get; set; }
		public IFormFile Attachment { get; set; } // 파일 첨부를 위한 속성 추가


		// Validation
		public bool HasError { get; set; } = false;  // 검증오류존재여부
        public string? ErrorTitle { get; set; }   // '작성자' 관련 검증 오류메세지
        public string? ErrorContent { get; set; } // '제목' 관련 검증 오류메세지

        public void ErrorCheck()
        {
            if (Title.IsNullOrEmpty())
            {
                ErrorTitle = "제목을 작성해 주십시오.";
                HasError = true;
            }

            if (Content.IsNullOrEmpty())
            {
                ErrorContent = "내용을 작성해 주십시오.";
                HasError = true;
            }
            else if (Content.Length < 10)
            {
                ErrorContent = "내용은 10글자 이상 입력해야 합니다.";
                HasError = true;
            }
        }
    }
}
