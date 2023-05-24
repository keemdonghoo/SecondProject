using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamProject.Models.Domain
{
    [Table("Attachment")]
    public class Attachment
    {
        public long Id { get ; set; }

		public string Title { get; set; }

		public string OriginalName { get; set; }  // 원본파일명
        public string FileName { get; set; }  // 저장된 파일명

        public Post Post { get; set; }
        public long PostId { get; set; }

		[NotMapped]  // <- migration 에 반영되지 않는 property
		public long Size { get; set; }  // 파일 용량
		[NotMapped]
		public string? ContentType { get; set; }  // 파일 종류 (MIME type)        
		[NotMapped]
		public string? RequestPath { get; set; }  // 저장된 파일에 대한 요청 경로 (url)   
		[NotMapped]
		public bool IsImage { get; set; }  // 이미지 여부
		[NotMapped]
		public string? Description { get; set; }  // 부가정보
	}
}
