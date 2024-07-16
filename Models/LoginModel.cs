using System.ComponentModel.DataAnnotations;

namespace EmprestimosAPI.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
