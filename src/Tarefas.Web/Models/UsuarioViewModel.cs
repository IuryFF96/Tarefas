using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Tarefas.Web.Models
{
    public class UsuarioViewModel
    {    
        [Required(ErrorMessage = "O email deve ser preenchido.")]
        [MinLength(5, ErrorMessage = "O email deve ter no mínimo 5 caracteres.")]
        [MaxLength(100, ErrorMessage = "O email pode ter no máximo 100 caracteres.")]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O nome deve ser preenchido.")]
        [MinLength(5, ErrorMessage = "O nome deve ter no mínimo 3 caracteres.")]
        [MaxLength(100, ErrorMessage = "O nome pode ter no máximo 100 caracteres.")]
        [DisplayName("Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A senha deve ser preenchida.")]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "A senha deve ter no mínimo 8 caracteres.")]
        [MaxLength(100, ErrorMessage = "A senha pode ter no máximo 30 caracteres.")]
        [DisplayName("Senha")]
        public string Senha { get; set; }

        [DisplayName("Ativo")]
        public bool Ativo { get; set; }
    } 
}   