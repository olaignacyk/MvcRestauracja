// User.cs
using System.ComponentModel.DataAnnotations;

namespace MvcRestauracja.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool Admin {get; set;}
    }
}
