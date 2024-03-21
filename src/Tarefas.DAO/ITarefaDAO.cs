using Tarefas.DTO;

public interface ITarefaDAO
{
    void Criar(TarefaDTO tarefa);
    List<TarefaDTO> Consultar(int usuarioid);
    TarefaDTO Detalhar(int Id, int usuarioid);
    void Excluir(int Id, int usuarioid);
    void Editar(TarefaDTO tarefa);
}