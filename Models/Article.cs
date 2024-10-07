using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace razorwebapp_sql.Models
//  razorwebapp_sql.Models.Article
{
    public class Article
    {
        [Key]
        public int ID { get; set; }

        [StringLength(255, MinimumLength = 5, ErrorMessage = "{0} phải dài từ {2} đến {1} kí tự")]
        [Required(ErrorMessage = "{0} phải nhập")]
        [Column(TypeName = "nvarchar")]
        [DisplayName("Tiêu đề")]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Ngày tạo")]
        public DateTime PublishDate { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Nội dung")]
        public string Content {set; get;}
    }
}