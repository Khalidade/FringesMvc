using System.ComponentModel.DataAnnotations;

namespace FringesMVC.Models
{
    public class LoginViewModel
    {

        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Display(Name = "Remember me")]
        public bool Rememberme { get; set; }
    }
}
