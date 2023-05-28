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
		public string PassWordCheck { get; set; }
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
			
            Regex regex = new Regex(@"^(?=.*[A-Z])(?=.*[!@#$%^&*()_+\-=[\]{};':""\\|,.<>/?]).{8,}$");
            if (!PassWord.IsNullOrEmpty())
			{
                if (!regex.IsMatch(PassWord))
                {
                    ErrorPassWord = "비밀번호는 대문자,특수문자 포함 8자리이상";
                    HasError = true;
                }
                else if (PassWord != PassWordCheck)
                {
                    ErrorPassWord = "비밀번호가 일치하지 않습니다.";
                    HasError = true;
                }
            }

			if (UserName.IsNullOrEmpty())
			{
				ErrorUserName = "이름 입력은 필수입니다";
				HasError = true;
			}

            regex = new Regex(@"^01([0|1|6|7|8|9]?)?([0-9]{3,4})?([0-9]{4})$");
            if (PhoneNum.IsNullOrEmpty())
			{
				ErrorPhoneNum = "휴대폰 번호 입력은 필수입니다";
				HasError = true;
			}
            else if (!regex.IsMatch(PhoneNum))
            {
                ErrorPhoneNum = "올바른 전화번호가 아닙니다.";
                HasError = true;
            }


			if (NickName.IsNullOrEmpty())
			{
				ErrorNickName = "닉네임 입력은 필수입니다";
				HasError = true;
			}

            if (Email != null)
            {
                regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
                if (!regex.IsMatch(Email))
                {
                    ErrorEmail = "올바른 이메일이 아닙니다.";
                    HasError = true;
                }
            }
        }

	
	}
}