using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using TeamProject.Models.Domain;

namespace TeamProject.Models.ViewModels
{
	public class AddCommentRequest
	{
		// 댓글 내용
		public string Content { get; set; }
	

		// Validation
		public bool HasError { get; set; } = false;  // 검증오류존재여부
		public string? ErrorContent { get; set; } // '댓글' 관련 검증 오류메세지

		public void Validate()
		{

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
