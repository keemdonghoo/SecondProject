namespace TeamProject.Models.ViewModels
{
    public class EditUserRequest
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string PassWord { get; set; }
        public string UserName { get; set; }
        public string PhoneNum { get; set; }
        public string NickName { get; set; }
        public string? Email { get; set; }
    }
}
