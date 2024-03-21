using Dapper;
using System;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using Tarefas.DTO;
using System.Collections.Generic;

namespace Tarefas.DAO

{
    public class TarefaDAO : BaseDAO,ITarefaDAO
    {
        public TarefaDAO()
        {

        }
        public void Criar(TarefaDTO tarefa)
        {
            using (var con = Connection)
            {
                con.Open();
                con.Execute
                (
                    @"INSERT INTO Tarefa
                    (Titulo, Descricao, Status, UsuarioId) VALUES
                    (@Titulo, @Descricao, @Status, @usuarioid);", tarefa
                );
            }
        }
        public List<TarefaDTO> Consultar(int usuarioid)
        {
            using (var con = Connection)
            {
                con.Open();
                var result = con.Query<TarefaDTO>
                (
                    @"SELECT Id, Titulo, Descricao, Status, UsuarioId FROM Tarefa WHERE UsuarioId = @usuarioid", new {usuarioid}
                ).ToList();
                return result;
            }
        }
        public TarefaDTO Detalhar(int Id, int usuarioid)
        {
            using (var con = Connection)
            {
                con.Open();
                TarefaDTO result = con.Query<TarefaDTO>
                (
                    @"SELECT Id, Titulo, Descricao, Status FROM Tarefa
                    WHERE Id = @Id AND UsuarioId = @usuarioid", new { Id, usuarioid }
                ).FirstOrDefault();
                return result;               
            }
        }
        public void Excluir(int Id, int usuarioid)
        {
            using (var con = Connection)
            {
                con.Open();
                con.Execute
                (
                    @"DELETE FROM Tarefa
                    WHERE Id = @Id AND UsuarioId = @usuarioid", new { Id, usuarioid }
                );
            }
        }
        public void Editar(TarefaDTO tarefa)
        {
            using (var con = Connection)
            {
                con.Open();
                con.Execute
                (
                    @"UPDATE Tarefa SET Titulo = @Titulo, Descricao = @Descricao, Status = @Status
                    WHERE Id = @Id", tarefa
                );
            }
        }
    }    
}