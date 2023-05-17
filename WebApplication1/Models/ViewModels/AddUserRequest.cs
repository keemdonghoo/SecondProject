using Microsoft.IdentityModel.Tokens;

namespace TeamProject.Models.ViewModels
{
    public class AddUserRequest
    {
        public string Name { get; set; }
        public string PassWord { get; set; }
        public string UserName { get; set; }
        public string PhoneNum { get; set; }
        public string NickName { get; set; }
        public string? Email { get; set; }

        public bool HasError { get; set; } = false;
        //-=----------------------------------------------------------------------------------------------------------------------------------//
        
        public string? ErrorName { get; set; } // ID
        public string? ErrorPassWord { get; set; } // 비밀번호
        public string? ErrorUserName { get; set; } // 사용자 이름
        public string? ErrorPhoneNum { get; set; } // 전화번호
        public string? ErrorNickName { get; set; } // 비밀번호

        //-=----------------------------------------------------------------------------------------------------------------------------------//

        public void Validate()
        {
            if (Name.IsNullOrEmpty())
            {
                ErrorName = "아이디입력은 필수입니다.";
                HasError = true;
            }
            if (PassWord.IsNullOrEmpty())
            {
                ErrorPassWord = "Password입력필수";
                HasError = true;
            }
            if (ErrorUserName.IsNullOrEmpty())
            {
                ErrorUserName = "사용자 이름입력 필수";
                HasError = true;
            }
            if (ErrorPhoneNum.IsNullOrEmpty())
            {
                ErrorPhoneNum = "전화번호 입력 필수";
                HasError = true;
            }
            if (NickName.IsNullOrEmpty()) 
            {
                ErrorNickName = "닉네임을 입력하새요";
                HasError = true;
            }
        }


    }
}
