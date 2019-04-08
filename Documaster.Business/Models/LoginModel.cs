using System.ComponentModel.DataAnnotations;

namespace Documaster.Business.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Numele de utilizator nu poate fi gol")]
        [Display(Name = "Nume utilizator")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Parola nu poate fi goala")]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        public string Password { get; set; }

        [Display(Name = "Te tin autentificat?")]
        public bool RememberMe { get; set; }
    }
}
