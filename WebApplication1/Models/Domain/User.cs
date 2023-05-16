﻿using System.ComponentModel;

namespace WebApplication1.Models.Domain
{
    public class User
    {
        
        public long Uid { get; set; }
        public string Name { get; set; }
        public string PassWord { get; set; }
        public string UserName { get; set; }
        public string PhoneNum { get; set; }
        public string NickName { get; set; }

        [DefaultValue(false)]
        public bool IsAdmin { get; set; }
      

        public string? Email { get; set; }
    }
}
