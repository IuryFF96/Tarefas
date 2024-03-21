using Dapper;
using System;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using Tarefas.DTO;
using System.Collections.Generic;

namespace Tarefas.DAO

{
    public class AgendaDAO : BaseDAO,IAgendaDAO
    {
        public AgendaDAO()
        {

        }
        public void CriarAgenda(AgendaDTO agenda)
        {
            using (var con = Connection)
            {
                con.Open();
                con.Execute
                (
                    @"INSERT INTO Agenda
                    (Titulo, Descricao, Data, Hora, Status, UsuarioId) VALUES
                    (@Titulo, @Descricao, @Data, @Hora, 'Pendente', @usuarioid);", agenda
                );
            }
        }
        public List<AgendaDTO> ConsultarAgenda(int usuarioid)
        {
            using (var con = Connection)
            {
                con.Open();
                var result = con.Query<AgendaDTO>
                (
                    @"SELECT Id, Titulo, Descricao, Data, Hora, Status, UsuarioId FROM Agenda WHERE UsuarioId = @usuarioid", new {usuarioid}
                ).ToList();
                return result;
            }
        }
        public AgendaDTO DetalharAgenda(int Id, int usuarioid)
        {
            using (var con = Connection)
            {
                con.Open();
                AgendaDTO result = con.Query<AgendaDTO>
                (
                    @"SELECT Id, Titulo, Descricao, Data, Hora, Status, UsuarioId FROM Agenda
                    WHERE Id = @Id AND UsuarioId = @usuarioid", new { Id, usuarioid }
                ).FirstOrDefault();
                return result;               
            }
        }
        public void ExcluirAgenda(int Id, int usuarioid)
        {
            using (var con = Connection)
            {
                con.Open();
                con.Execute
                (
                    @"DELETE FROM Agenda
                    WHERE Id = @Id AND UsuarioId = @usuarioid", new { Id, usuarioid }
                );
            }
        }
        public void EditarAgenda(AgendaDTO agenda)
        {
            using (var con = Connection)
            {
                con.Open();
                con.Execute
                (
                    @"UPDATE Agenda SET Titulo = @Titulo, Descricao = @Descricao, Data = @Data, Hora = @Hora, Status = @Status
                    WHERE Id = @Id", agenda
                );
            }
        }
        public AgendaDTO ValidarAgenda(AgendaDTO agenda)
        {
            using (var con = Connection)
            {
                con.Open();
                AgendaDTO result = con.Query<AgendaDTO>
                (
                   @"SELECT * FROM Agenda WHERE Data = @Data AND Hora = @Hora AND UsuarioId = @usuarioid", agenda
                ).FirstOrDefault();

                return result;
            }
        }

        public AgendaDTO ValidarEditAgenda(AgendaDTO agenda)
        {
            using (var con = Connection)
            {
                con.Open();
                AgendaDTO result = con.Query<AgendaDTO>
                (
                   @"SELECT * FROM Agenda WHERE Data = @Data AND Hora = @Hora AND Id <> @Id", agenda
                ).FirstOrDefault();

                return result;
            }
        }
    }    
}