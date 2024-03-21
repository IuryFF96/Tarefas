using Dapper;
using System;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using Tarefas.DTO;
using System.Collections.Generic;

namespace Tarefas.DAO

{
    public class UsuarioDAO : BaseDAO,IUsuarioDAO
    {
        public UsuarioDAO()
        {

        }
        public void CriarUsuario(UsuarioDTO usuario)
        {
            using (var con = Connection)
            {
                con.Open();
                con.Execute
                (
                    @"INSERT INTO Usuario
                    (Email, Nome, Senha, Ativo) VALUES
                    (@Email, @Nome, @Senha, true);", usuario
                );
            }
        }
        public UsuarioDTO ValidarUsuario(UsuarioDTO usuario)
        {
            using (var con = Connection)
            {
                con.Open();
                UsuarioDTO result = con.Query<UsuarioDTO>
                (
                   @"SELECT * FROM Usuario WHERE Email = @Email", usuario
                ).FirstOrDefault();

                return result;
            }
        }
        public UsuarioDTO ValidarLogin(UsuarioDTO usuario)
        {
            using (var con = Connection)
            {
                con.Open();
                UsuarioDTO result = con.Query<UsuarioDTO>
                (
                   @"SELECT * FROM Usuario WHERE Email = @Email and Senha = @Senha and Ativo = true", usuario
                ).FirstOrDefault();

                return result;
            }
        }

    }
    
}