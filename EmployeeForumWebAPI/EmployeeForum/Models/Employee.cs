using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeForum.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName ="varchar(100)")]
        public string FullName { get; set; }

        [Required]        
        [Column(TypeName = "varchar(50)")]
        public string UserName { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Password { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Email { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string Photo { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string MobileNo { get; set; }

        [Required]
        [Column(TypeName = "char")]
        public char Gender { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastLoggedInDate { get; set; }
        

    }
}
