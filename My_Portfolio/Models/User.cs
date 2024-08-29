using System.ComponentModel.DataAnnotations;

namespace My_Portfolio.Models
{
    public class User
    {
        [Key]
        public int User_Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public long Mobile { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public bool IsRemember { get; set; }

    }
}
