using System.ComponentModel.DataAnnotations;

namespace Session.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Имя пользователя: ")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Пароль: ")]
        public string Password { get; set; }
    }
}
