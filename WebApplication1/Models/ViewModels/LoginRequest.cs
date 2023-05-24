using Microsoft.IdentityModel.Tokens;

namespace TeamProject.Models.ViewModels
{
    public class LoginRequest
    {
        public string Name { get; set; }
        public string PassWord { get; set; }

        public bool HasError { get; set; } = false;
        //-=----------------------------------------------------------------------------------------------------------------------------------//
        public string? ErrorName { get; set; } // ID
        public string? ErrorPassWord { get; set; } // 비밀번호
        public void CheckLogin()
        {
            if (Name.IsNullOrEmpty())
            {
                ErrorName = "아이디를 입력하세요";
                HasError = true;
            }
            if(PassWord.IsNullOrEmpty())
            {
                ErrorPassWord = "비밀번호를 입력하세요";
                HasError = true;
            }
        }
    }
}
