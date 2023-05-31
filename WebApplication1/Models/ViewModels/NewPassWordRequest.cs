using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace TeamProject.Models.ViewModels
{
    public class NewPassWordRequest
    {
        public string PassWord { get; set; }
        public string PassWordCheck { get; set; }

        public string? ErrorPassWord { get; set; }   // 패스워드 관련 검증 

        public bool HasError { get; set; } = false;  // 검증오류존재여부
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
        }
    }
}
