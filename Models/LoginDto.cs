using System.ComponentModel.DataAnnotations;

namespace MakeupShop.Models
{
    public class LoginDto
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}
