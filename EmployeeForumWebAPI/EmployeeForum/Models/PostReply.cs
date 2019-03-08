using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeForum.Models
{
    public class PostReply
    {
        [Key]
        public int PostReplyId { get; set; }

        [Required]
        [ForeignKey("PostId")]
        public int PostId { get; set; }
 

        [Required]
        [Column(TypeName = "varchar(250)")]
        public string ReplyWithAnswer { get; set; }

        [Required]
        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
        
        [Required]
        public DateTime CreatedDate { get; set; }
    }
}
