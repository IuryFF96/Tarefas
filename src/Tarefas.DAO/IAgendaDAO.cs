using Tarefas.DTO;

public interface IAgendaDAO
{
    void CriarAgenda(AgendaDTO agenda);
    List<AgendaDTO> ConsultarAgenda(int usuarioid);
    AgendaDTO DetalharAgenda(int Id, int usuarioid);
    void ExcluirAgenda(int Id, int usuarioid);
    void EditarAgenda(AgendaDTO agenda);
    AgendaDTO ValidarAgenda(AgendaDTO agenda);
    AgendaDTO ValidarEditAgenda(AgendaDTO agenda);
}