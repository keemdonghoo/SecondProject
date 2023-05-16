﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models.Domain;

namespace TeamProject.Models.Domain
{
    [Table("Attachment")]
    public class Attachment
    {
        public long AttachmentUID { get ; set; }

        public string OriginalName { get; set; }  // 원본파일명
        public string FileName { get; set; }  // 저장된 파일명

        public Post Post { get; set; }
        public long PostUID { get; set; }
    }
}
