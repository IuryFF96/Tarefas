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
                    (Titulo, Descricao, Status) VALUES
                    (@Titulo, @Descricao, @Status);", tarefa
                );
            }
        }
        public List<TarefaDTO> Consultar()
        {
            using (var con = Connection)
            {
                con.Open();
                var result = con.Query<TarefaDTO>
                (
                    @"SELECT Id, Titulo, Descricao, Status FROM Tarefa"
                ).ToList();
                return result;
            }
        }
        public TarefaDTO Detalhar(int Id)
        {
            using (var con = Connection)
            {
                con.Open();
                TarefaDTO result = con.Query<TarefaDTO>
                (
                    @"SELECT Id, Titulo, Descricao, Status FROM Tarefa
                    WHERE Id = @Id", new { Id }
                ).FirstOrDefault();
                return result;               
            }
        }
        public void Excluir(int Id)
        {
            using (var con = Connection)
            {
                con.Open();
                con.Execute
                (
                    @"DELETE FROM Tarefa
                    WHERE Id = @Id", new { Id }
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