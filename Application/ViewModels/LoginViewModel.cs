using System.ComponentModel.DataAnnotations;

namespace Fisilti.MVC.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Kullanıcı Adı Zorunlu")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Kullanıcı Adı Zorunlu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }


    }
}
