using System.ComponentModel.DataAnnotations;

namespace My_Portfolio.Models
{
    public class Employees
    {
        public int Employee_Id { get; set; }

        [Required]
        public string? Employee_Name { get; set; }

        [Required]
        public string? Gender { get; set; }
        
        [Required]
        public int Age { get; set; }
        
        [Required]
        public string? Designation { get; set; }
        
        [Required]
        public string? City { get; set; }
    }
}
