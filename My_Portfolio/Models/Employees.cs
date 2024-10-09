using System.ComponentModel.DataAnnotations;

namespace My_Portfolio.Models
{
    public class Employees
    {
        [Key]
        public int EmployeeLog_Id { get; set; }

        [Required(ErrorMessage = "Firstname is required")]
        public string? Firstname { get; set; }

        [Required(ErrorMessage = "Lastname is required")]
        public string? Lastname { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(1, 100, ErrorMessage = "Please enter a valid age")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        public string? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        public string? Phone { get; set; }


        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        
        [Required(ErrorMessage = "Confirm Password is required")]
        public string? ConfirmPassword { get; set; }

        
        [Required(ErrorMessage = "Address is required")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string? City { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string? State { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string? Country { get; set; }

        [Required(ErrorMessage = "Zipcode is required")]
        [Range(100000, 999999, ErrorMessage = "Invalid Zipcode")]
        public int Zipcode { get; set; }
    }
}

