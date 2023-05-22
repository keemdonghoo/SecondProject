using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace TeamProject.Models.ViewModels
{
    public class AddUserRequest
    {
       
        public string Name { get; set; }
        public bool NameCheck { get; set; }
        public string PassWord { get; set; }
        public string PassWordCheck { get; set; }
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
        public string? ErrorEmail { get; set; } // 비밀번호

        //-=----------------------------------------------------------------------------------------------------------------------------------//
      
        public void Validate()
        {
            if (NameCheck)
            {
                ErrorName = "아이디중복확인";
                HasError = true;
            }
            //-----------------비밀번호 검증 -----------------//
            
            Regex regex = new Regex(@"^(?=.*[A-Z])(?=.*[!@#$%^&*()_+\-=[\]{};':""\\|,.<>/?]).{8,}$");

            if (PassWord.IsNullOrEmpty())
            {
                ErrorPassWord = "Password입력필수";
                HasError = true;
            }
            else if (!regex.IsMatch(PassWord))
            {
                ErrorPassWord = "비밀번호는 대문자,특수문자 포함 8자리이상";
                HasError = true;
            }
            else if (PassWord == PassWordCheck)
            {
                ErrorPassWord = "비밀번호가 일치하지 않습니다.";
                HasError = true;
            }

            //-------이름검증
            if (UserName.IsNullOrEmpty())
            {
                ErrorUserName = "사용자 이름입력 필수";
                HasError = true;
            }

            //-----------전화번호 검증
            regex = new Regex(@"^(\+82|0)[1-9]\d{1,2}-\d{3,4}-\d{4}$");
            if (PhoneNum.IsNullOrEmpty())
            {
                ErrorPhoneNum = "전화번호 입력 필수";
                HasError = true;
            }
            else if (!regex.IsMatch(PhoneNum))
            {
                ErrorPassWord = "올바른 전화번호가 아닙니다.";
                HasError = true;
            }

            //-----닉네임 검증
            if (NickName.IsNullOrEmpty()) 
            {
                ErrorNickName = "닉네임을 입력하새요";
                HasError = true;
            }
            //-------이메일 검증 //^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$
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
