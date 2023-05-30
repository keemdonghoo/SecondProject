﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApplication1.Models.ViewMoldels
{
    public class AddUserRequest
    {
        public string Name { get; set; }
        public string PassWord { get; set; }
        public string UserName { get; set; }
        public string PhoneNum { get; set; }
        public string NickName { get; set; }
        public string? Email { get; set; }
    }
}