using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualBasic;

namespace Tarefas.Web.Models
{
    public class AgendaViewModel
    {    
        [DisplayName("Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O título deve ser preenchido.")]
        [MinLength(5, ErrorMessage = "O título deve ter no mínimo 5 caracteres.")]
        [MaxLength(100, ErrorMessage = "O título pode ter no máximo 100 caracteres.")]
        [DisplayName("Título")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "A descrição deve ser preenchida.")]
        [MinLength(5, ErrorMessage = "A descrição deve ter no mínimo 5 caracteres.")]
        [MaxLength(100, ErrorMessage = "A descrição pode ter no máximo 100 caracteres.")]
        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "A data deve ser preenchida.")]
        [DisplayName("Data")]
        public string Data { get; set; }

        [Required(ErrorMessage = "A hora deve ser preenchida.")]
        [DisplayName("Hora")]
        public string Hora { get; set; }
        
        [DisplayName("Status")]
        public string? Status { get; set; }
        public int UsuarioId { get; set; }
    } 
}