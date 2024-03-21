using Tarefas.DTO;

public interface IUsuarioDAO
{
    void CriarUsuario(UsuarioDTO usuario);
    UsuarioDTO ValidarUsuario(UsuarioDTO usuario);
    UsuarioDTO ValidarLogin(UsuarioDTO usuario);
}