using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Tarefas.Web.Models
{
    public class LoginViewModel
    {    
        [Required(ErrorMessage = "O email deve ser preenchido.")]
        [MinLength(5, ErrorMessage = "O email deve ter no mínimo 5 caracteres.")]
        [MaxLength(100, ErrorMessage = "O email pode ter no máximo 100 caracteres.")]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha deve ser preenchida.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        [MaxLength(20, ErrorMessage = "A senha pode ter no máximo 20 caracteres.")]
        [DisplayName("Senha")]
        public string Senha { get; set; }
    } 
}   