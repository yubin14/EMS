using System.ComponentModel.DataAnnotations;

namespace EMS.Models
{
    public class UpdateEmployeeViewModel
    {
        
        public int EmployeeID { get; set; }

        public string FirstName { get; set; } = string.Empty;

       
        public string LastName { get; set; } = string.Empty;

        
        public string Email { get; set; } = string.Empty;

      
        public string Department { get; set; } = string.Empty;
    }
}

