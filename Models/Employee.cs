using System.ComponentModel.DataAnnotations;

namespace EMS.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty; 

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty; 

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty; 

        [Required]
        [MaxLength(50)]
        public string Department { get; set; } = string.Empty;
    }
}
