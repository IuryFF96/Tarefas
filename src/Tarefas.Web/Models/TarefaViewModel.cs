using System.ComponentModel;

namespace Tarefas.Web.Models
{
    public class TarefaViewModel
    {
        public int Id { get; set; }
        [DisplayName("Descrição")]
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        [DisplayName("Títulos")]
        public string Status { get; set; }
    } 
}   