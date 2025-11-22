using System.ComponentModel.DataAnnotations;

namespace Fisilti.MVC.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "İsim Alanı Zorunlu")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "E-Posta Zorunlu")]
        [DataType(DataType.EmailAddress,ErrorMessage = "Lütfen Geçerli Bir E-Posta Girin")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Kullanıcı Adı Zorunlu.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Parola Zorunlu")]
        [DataType(DataType.Password,ErrorMessage = "Lütfen Geçerli Bir Parola Girin")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Parola Tekrarı Zorunlu")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Girilen Parolalar Eşleşmiyor")]
        public string ConfirmPassword { get; set; }



    }
}
