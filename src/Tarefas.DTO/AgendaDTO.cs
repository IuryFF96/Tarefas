using System;

namespace Tarefas.DTO
{
    public class AgendaDTO
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public string Data { get; set; }
        public string Hora { get; set; }
        public string Status { get; set; }
        public int UsuarioId { get; set; }
    }
}