using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace TeamProject.Models.ViewModels
{
	public class EditUserRequest
	{

		private static readonly Regex PhoneNumRegex = new Regex(@"^01[0-9]{8,9}$");
		private static readonly Regex EmailRegex = new Regex(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");

		public long UserId { get; set; }
		public string Name { get; set; }
		public string PassWord { get; set; }
		public string UserName { get; set; }
		public string PhoneNum { get; set; }
		public string NickName { get; set; }
		public string? Email { get; set; }

		public bool HasError { get; set; } = false;  // 검증오류존재여부

		public string? ErrorName { get; set; }   //  아이디 관련 검증 
		public string? ErrorPassWord { get; set; }   // 패스워드 관련 검증 
		public string? ErrorUserName { get; set; }   // 이름 관련 검증 
		public string? ErrorPhoneNum { get; set; }   // 번호 관련 검증 
		public string? ErrorNickName { get; set; }   // 닉네임 관련 검증 
		public string? ErrorEmail { get; set; }   // 이메일 관련 검증 

		public void Validate()
		{
			if (Name.IsNullOrEmpty())
			{
				ErrorName = "아이디 입력은 필수입니다";
				HasError = true;
			}

			if (PassWord.IsNullOrEmpty())
			{
				ErrorPassWord = "패스워드 입력은 필수입니다";
				HasError = true;
			}

			if (UserName.IsNullOrEmpty())
			{
				ErrorUserName = "이름 입력은 필수입니다";
				HasError = true;
			}

			if (PhoneNum.IsNullOrEmpty())
			{
				ErrorPhoneNum = "휴대폰 번호 입력은 필수입니다";
				HasError = true;
			}

			else if (!PhoneNumRegex.IsMatch(PhoneNum))
			{
				ErrorPhoneNum = "올바른 휴대폰 번호 형식으로 입력해주세요";
				HasError = true;
			}

			if (NickName.IsNullOrEmpty())
			{
				ErrorNickName = "닉네임 입력은 필수입니다";
				HasError = true;
			}

			if (Email.IsNullOrEmpty())
			{
				ErrorEmail = "이메일 입력은 필수입니다";
				HasError = true;
			}

			else if (!EmailRegex.IsMatch(Email))
			{
				ErrorEmail = "올바른 이메일 형식으로 입력해주세요";
				HasError = true;
			}
		}

	
	}
}