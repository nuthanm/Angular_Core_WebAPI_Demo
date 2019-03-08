using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeForum.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        [Required]
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }        

        [Required]
        [Column(TypeName = "varchar(250)")]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "varchar(max)")]
        public string Description { get; set; }

        [Required]
        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
        
        [Required]
        public DateTime CreatedDate { get; set; }


    }
}
